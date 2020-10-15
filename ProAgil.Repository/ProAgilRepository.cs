using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain.Models;
using ProAgil.Repository.Data;
using ProAgil.Repository.Interface;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilDataContext _context;
        public ProAgilRepository(ProAgilDataContext context)
        {
            _context = context;   
        }
        public void Add<T>(T entity) where T : class
        {
           _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
          _context.Update(entity);  
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity); 
        }
        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync() > 0);
        }

        //Evento
        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes){
                query = query 
                      .Include(pe => pe.PalestranteEventos)
                      .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c=> c.DataEvento)
                        .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();

        }

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes){
                query = query 
                      .Include(pe => pe.PalestranteEventos)
                      .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c=> c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePalestrantes)
        {
           IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

            if (includePalestrantes){
                query = query 
                      .Include(pe => pe.PalestranteEventos)
                      .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento)
                        .Where(c => c.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        //Palestrante
        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string Name, bool includeEventos)
        {
             IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

            if (includeEventos){
                query = query 
                      .Include(pe => pe.PalestranteEventos)
                      .ThenInclude(e => e.Evento);
            }

            query = query.OrderBy(p => p.Nome)
                         .Where(p => p.Nome.ToLower().Contains(Name.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestranteAsyncById(int PalestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

            if (includeEventos){
                query = query 
                      .Include(pe => pe.PalestranteEventos)
                      .ThenInclude(e => e.Evento);
            }

            query = query.OrderBy(p => p.Nome)
                         .Where(p => p.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}