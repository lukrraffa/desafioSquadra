using DesafioGerenciadorCursos.ValuesObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioGerenciadorCursos
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Titulo é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Duracao é obrigatório")]
        public DateTime Duracao { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public StatusCurso Status { get; set; }
    }
}
