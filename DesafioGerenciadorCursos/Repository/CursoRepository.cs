using DesafioGerenciadorCursos.Data;
using DesafioGerenciadorCursos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioGerenciadorCursos.Repository
{
    public class CursoRepository : IRepository
    {
        private readonly ApplicationContext _context;

        public CursoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Curso> ObterTodosCursos()
        {
            return _context.Curso.ToList();
        }
        public Curso ObterCursoPorId(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
