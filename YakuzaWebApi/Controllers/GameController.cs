using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using YakuzaWebApi.Models;

namespace YakuzaWebApi.Controllers
{
    public class GameController : ApiController
    {
        private YakuzaWebApiContext db = new YakuzaWebApiContext();

        // GET: api/Game
        public IQueryable<GameSession> GetGameSessions()
        {
            return db.GameSessions;
        }
        [Route("api/game/CreateUser/{username}")]
        [HttpGet]
        public IHttpActionResult CreateUser(string username)
        {
            User tryuser = db.Users.Where(u => u.Username == username).FirstOrDefault();
            if (tryuser != null)
            {
                return Json("Już jest taki username");
            }
            else
            {
                try
                {
                    User user = new User()
                    {
                        Username = username,
                        isInMafia = false,
                        isKilled = false
                    };
                    db.Users.Add(user);

                    //db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(user);
                }
                catch (Exception ex)
                {
                    return Json(String.Format("Error !  = {0}", ex));
                }

            }
        }
        // GET: api/Game/5
        [Route("api/game/GetGameSession/{id}")]
        [HttpGet]
        public IHttpActionResult GetGameSession(int id)
        {

            GameSession gameSession = db.GameSessions.Find(id);
            gameSession.Users = db.Users.Where(u => u.SessionId == id).ToList();
            gameSession.GameEvents = db.GameEvents.Where(u => u.GameSessionId == id).ToList();

            //.Select(ge => ge.GameSessionId == id)
            //if (gameSession == null)
            //{
            //    return NotFound();
            //}
            if (gameSession != null)
            {
                return Json(gameSession);
            }
            else
            {
                return Json(String.Format("Error ! Id = {0}", id));
            }
        }
        [Route("api/game/GetGameEvent/{id}")]
        [HttpGet]
        public IHttpActionResult GetGameEvent(int id)
        {
            try
            {
                return Json(db.GameEvents.Find(id));
            }
            catch (Exception)
            {
                return Json("nope");
            }
        }
        [HttpGet]
        public IHttpActionResult GetSessions()
        {
            return Json(db.GameSessions);
        }

        // GET : Create and get session for user
        [HttpGet]
        public IHttpActionResult CreateGameSession(string username)
        {
            GameSession gamesession;
            try
            {
                User user = db.Users.Where(u => u.Username == username).FirstOrDefault();
                gamesession = new GameSession()
                {
                    Id = new int(),
                    IsActive = true,
                    IsNight = false,
                };
                db.GameSessions.Add(gamesession);
                user.SessionId = gamesession.Id;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(String.Format("Error ! Username = {0}", username));
            }

            return Json(gamesession);
        }
        [HttpGet]
        public IHttpActionResult JoinSession(string username, int sessionid)
        {
            User user = db.Users.Where(u => u.Username == username).FirstOrDefault();

            GameSession gameSession = db.GameSessions.Find(sessionid);
            gameSession.Users = db.Users.Where(u => u.SessionId == sessionid).ToList();
            gameSession.GameEvents = db.GameEvents.Where(u => u.GameSessionId == sessionid).ToList();
            if (gameSession.Users.Count < 6)
            {
                try
                {
                    user.SessionId = sessionid;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    if (gameSession.Users.Count == 5)
                    {
                        gameSession.IsActive = true;
                    }
                    db.Entry(gameSession).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(String.Format("User joined session {0}", sessionid));
                }

                catch (Exception)
                {
                    return Json(String.Format("Error ! Username = {0}, SessionId = {1}", username, sessionid));
                }
            }
            else
            {
                return Json("Gra pełna");
            }

        }
        [Route("api/game/VoteUser/{votedusername}/{gamesessionid}")]
        [HttpGet]
        public IHttpActionResult VoteUser(string VotedUsername, int GameSessionId)
        {
            Vote vote = new Vote()
            {
                VotedUsername = VotedUsername,
                GameSessionId = GameSessionId
            };
            db.Votes.Add(vote);
            db.SaveChanges();
            return Json("success");
        }
        [Route("api/game/GetVotes/{gamesessionid}")]
        [HttpGet]
        public IHttpActionResult GetVotes(int GameSessionId)
        {
            return Json(db.Votes.Where(v => v.GameSessionId == GameSessionId).ToList());
        }
        [Route("api/game/KillUser/{username}/{gamesessionid}")]
        [HttpGet]
        public IHttpActionResult KillUser(string username, int GameSessionId)
        {
            List<Vote> votesToRemove = db.Votes.Where(v => v.GameSessionId == GameSessionId).ToList();
            User user = db.Users.Where(u => u.Username == username).FirstOrDefault();
            if (user != null)
            {
                user.isKilled = true;
                db.Entry(user).State = EntityState.Modified;

                foreach (var vote in votesToRemove)
                {
                    db.Votes.Remove(vote);
                }
                db.SaveChanges();
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }
        [Route("api/game/SetMafiosoForGame/{username1}/{username2}/{gamesessionid}")]
        [HttpGet]
        public IHttpActionResult SetMafiosoForGame(string username1, string username2, int gamesessionid)
        {
            try
            {
                User user1 = db.Users.Where(u => u.Username == username1).FirstOrDefault();
                User user2 = db.Users.Where(u => u.Username == username2).FirstOrDefault();
                if (user1 != null && user2 != null)
                {
                    user1.isInMafia = true;
                    user2.isInMafia = true;
                    db.Entry(user1).State = EntityState.Modified;
                    db.Entry(user2).State = EntityState.Modified;
                }

                db.SaveChanges();
                return Json(String.Format("Mafia set up"));
            }
            catch (Exception)
            {
                return Json(String.Format("not this time, bitch"));
            }
        }

        //[Route("api/game/addgameevent/{gameevent}")]
        //[HttpGet]
        //public IHttpActionResult AddGameEvent(GameEvent gameEvent)
        //{
        //    try
        //    {
        //        db.GameEvents.Add(gameEvent);
        //        db.SaveChanges();
        //        return Json("success");
        //    }
        //    catch (Exception)
        //    {
        //        return Json(String.Format("error with parameter = {0}", Json(gameEvent)));
        //    }
        //}
        [Route("api/game/GetUser/{username}")]
        [HttpGet]
        public IHttpActionResult GetUser(string username)
        {
            User user = db.Users.Where(u => u.Username == username).FirstOrDefault();
            if (user != null)
            {
                return Json(user);
            }
            else
            {
                return Json("ni ma go tu");
            }
        }

        [Route("api/game/addgameevent/{gamesessionid}/{textcontent}/{isonlyformafia}/{username}")]
        [HttpGet]
        public IHttpActionResult AddGameEvent(int gamesessionid, string textcontent, bool isonlyformafia, string username)
        {
            GameEvent gameEvent = new GameEvent()
            {
                GameSessionId = gamesessionid,
                TextContent = textcontent,
                Date = DateTime.Now,
                isOnlyForMafia = isonlyformafia,
                Username = username
            };

            try
            {
                db.GameEvents.Add(gameEvent);
                db.SaveChanges();
                return Json("success");
            }
            catch (Exception)
            {
                return Json(String.Format("error with parameter = {0}", Json(gameEvent)));
            }
        }

        // PUT: api/Game/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGameSession(int id, GameSession gameSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gameSession.Id)
            {
                return BadRequest();
            }

            db.Entry(gameSession).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameSessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Game
        [ResponseType(typeof(GameSession))]
        public IHttpActionResult PostGameSession(GameSession gameSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GameSessions.Add(gameSession);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GameSessionExists(gameSession.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = gameSession.Id }, gameSession);
        }

        // DELETE: api/Game/5
        [ResponseType(typeof(GameSession))]
        public IHttpActionResult DeleteGameSession(int id)
        {
            GameSession gameSession = db.GameSessions.Find(id);
            if (gameSession == null)
            {
                return NotFound();
            }

            db.GameSessions.Remove(gameSession);
            db.SaveChanges();

            return Ok(gameSession);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameSessionExists(int id)
        {
            return db.GameSessions.Count(e => e.Id == id) > 0;
        }
    }
}