using DesafioGerenciadorCursos.ValuesObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioGerenciadorCursos
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required (ErrorMessage = "Login é obrigatório")]
        public string Login { get; set; }

        [Required (ErrorMessage = "Senha é obrigatório")]
        public string Senha { get; set; }

        [Required (ErrorMessage = "Tipo pessoa é obrigatório")]
        public TipoPessoa TipoPessoa { get; set; }
    }
}
