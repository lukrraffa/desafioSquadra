using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioGerenciadorCursos;
using DesafioGerenciadorCursos.Data;
using DesafioGerenciadorCursos.Repository;
using Microsoft.AspNetCore.Authorization;

namespace DesafioGerenciadorCursos.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IRepository cursoRepository;

        public CursosController(ApplicationContext context, IRepository repository)
        {
            _context = context;
            cursoRepository = repository;
        }

        [AllowAnonymous]//Qualquer pessoa poder consultar todos os cursos
        [HttpGet]  // GET: api/Cursos
        public IActionResult GetCurso()
        {
            var retorno = cursoRepository.ObterTodosCursos();
            return Ok(retorno);
        }

        [AllowAnonymous]
        // GET: api/Cursos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var curso = await _context.Curso.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        [AllowAnonymous]// Qualquer pessoa poder consultar todos os cursos por status
        [HttpGet("{status}")]   // GET: api/Cursos/EmAndamento
        public async Task<ActionResult<Curso>> GetCurso(string status)
        {
            var curso = await _context.Curso.FindAsync(status);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        // só quem está autenticado pode atualizar
        [Authorize(Roles = "Gerente")]
        // PUT: api/Cursos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest();
            }

            _context.Entry(curso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [AllowAnonymous]
        // POST: api/Cursos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            _context.Curso.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurso", new { id = curso.Id }, curso);
        }

        // só quem está autenticado pode deletar
        [Authorize(Roles = "Gerente")]
        [HttpDelete("{id}")] // DELETE: api/Cursos/5
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _context.Curso.FindAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.Curso.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(int id)
        {
            return _context.Curso.Any(e => e.Id == id);
        }
    }
}
