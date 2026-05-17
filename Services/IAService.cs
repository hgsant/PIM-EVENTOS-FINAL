namespace EventosPRO.Web.Services
{
    public class IAService
    {
        public string AnalisarEvento(string descricao)
        {
            descricao = descricao.ToLower();

            if (descricao.Contains("rock") ||
                descricao.Contains("show"))
            {
                return "Categoria: Musical | Público: Jovens e Adultos";
            }

            if (descricao.Contains("aniversario") ||
                descricao.Contains("festa"))
            {
                return "Categoria: Festa | Público: Família";
            }

            if (descricao.Contains("casamento"))
            {
                return "Categoria: Cerimônia | Público: Adultos";
            }

            if (descricao.Contains("tecnologia") ||
                descricao.Contains("programacao"))
            {
                return "Categoria: Tecnologia | Público: Profissionais";
            }

            return "Categoria: Evento Geral | Público: Variado";
        }
    }
}