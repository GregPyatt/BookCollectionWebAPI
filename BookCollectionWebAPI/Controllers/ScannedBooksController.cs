using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BookCollectionWebAPI.Models;

namespace BookCollectionWebAPI.Controllers
{
    public class ScannedBooksController : ApiController
    {
        private BookCollectionEntities db = new BookCollectionEntities();

        // GET: api/ScannedBooks
        public IQueryable<ScannedBook> GetScannedBooks()
        {
            return db.ScannedBooks;
        }

        // GET: api/ScannedBooks/5
        [ResponseType(typeof(ScannedBook))]
        public async Task<IHttpActionResult> GetScannedBook(int id)
        {
            ScannedBook scannedBook = await db.ScannedBooks.FindAsync(id);
            if (scannedBook == null)
            {
                return NotFound();
            }

            return Ok(scannedBook);
        }

        // PUT: api/ScannedBooks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutScannedBook(int id, ScannedBook scannedBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scannedBook.BookID)
            {
                return BadRequest();
            }

            db.Entry(scannedBook).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScannedBookExists(id))
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

        // POST: api/ScannedBooks
        [ResponseType(typeof(ScannedBook))]
        public async Task<IHttpActionResult> PostScannedBook(ScannedBook scannedBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ScannedBooks.Add(scannedBook);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = scannedBook.BookID }, scannedBook);
        }

        // DELETE: api/ScannedBooks/5
        [ResponseType(typeof(ScannedBook))]
        public async Task<IHttpActionResult> DeleteScannedBook(int id)
        {
            ScannedBook scannedBook = await db.ScannedBooks.FindAsync(id);
            if (scannedBook == null)
            {
                return NotFound();
            }

            db.ScannedBooks.Remove(scannedBook);
            await db.SaveChangesAsync();

            return Ok(scannedBook);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScannedBookExists(int id)
        {
            return db.ScannedBooks.Count(e => e.BookID == id) > 0;
        }
    }
}