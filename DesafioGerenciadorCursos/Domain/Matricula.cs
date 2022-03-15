using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioGerenciadorCursos.Domain
{
    public class Matricula
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
