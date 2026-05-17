using EventosPRO.Web.Data;
using EventosPRO.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventosPRO.Web.Controllers
{
    [Authorize]
    public class EventosController : Controller
    {
        private readonly AppDbContext _context;

        public EventosController(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR EVENTOS
        [AllowAnonymous]
        public IActionResult Index(string busca)
        {
            var eventos = _context.Eventos.AsQueryable();

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                eventos = eventos.Where(e => e.UsuarioId == usuarioId);
            }

            if (!string.IsNullOrEmpty(busca))
            {
                eventos = eventos.Where(e =>
                    e.Nome.Contains(busca) ||
                    (e.Descricao != null && e.Descricao.Contains(busca)));
            }

            return View(eventos.ToList());
        }

        // TELA CRIAR
        public IActionResult Criar()
        {
            return View();
        }

        // SALVAR EVENTO
        [HttpPost]
        public IActionResult Criar(Evento evento)
        {
            if (ModelState.IsValid)
            {
                var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                evento.UsuarioId = usuarioId!;

                // IA AUTOMÁTICA
                evento.AnaliseIA =
                    $"Categoria: {(evento.Nome.Contains("festa", StringComparison.OrdinalIgnoreCase) ? "Festa" : "Evento Geral")} | " +
                    $"Público: {(evento.Descricao != null && evento.Descricao.Length > 20 ? "Família" : "Variado")}";

                evento.Data = DateTime.SpecifyKind(evento.Data, DateTimeKind.Utc);

                _context.Eventos.Add(evento);

                _context.SaveChanges();

                TempData["Sucesso"] = "Evento criado com sucesso!";

                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        // TELA EDITAR
        public IActionResult Editar(int id)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var evento = _context.Eventos
                .FirstOrDefault(e => e.Id == id && e.UsuarioId == usuarioId);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // SALVAR EDIÇÃO
        [HttpPost]
        public IActionResult Editar(Evento evento)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var eventoBanco = _context.Eventos
                .FirstOrDefault(e => e.Id == evento.Id && e.UsuarioId == usuarioId);

            if (eventoBanco == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                eventoBanco.Nome = evento.Nome;
                eventoBanco.Local = evento.Local;
                eventoBanco.Descricao = evento.Descricao;

                // IA AUTOMÁTICA
                eventoBanco.AnaliseIA =
                    $"Categoria: {(evento.Nome.Contains("festa", StringComparison.OrdinalIgnoreCase) ? "Festa" : "Evento Geral")} | " +
                    $"Público: {(evento.Descricao != null && evento.Descricao.Length > 20 ? "Família" : "Variado")}";

                eventoBanco.Data =
                    DateTime.SpecifyKind(evento.Data, DateTimeKind.Utc);

                _context.SaveChanges();

                TempData["Sucesso"] = "Evento editado com sucesso!";

                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        // EXCLUIR
        public IActionResult Excluir(int id)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var evento = _context.Eventos
                .FirstOrDefault(e => e.Id == id && e.UsuarioId == usuarioId);

            if (evento == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(evento);

            _context.SaveChanges();

            TempData["Sucesso"] = "Evento excluído com sucesso!";

            return RedirectToAction(nameof(Index));
        }
    }
}