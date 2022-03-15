using DesafioGerenciadorCursos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioGerenciadorCursos.Repository
{
    public interface IRepository
    {
        IEnumerable<Curso> ObterTodosCursos();
        Curso ObterCursoPorId(int id);
    }
}   
