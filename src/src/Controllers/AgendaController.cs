using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using System.Text;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        [HttpPost("gerar-ics")]
        public IActionResult GerarICS([FromBody] AgendamentoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sb = new StringBuilder();

            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:-//CaldasTI//GeradorICS 1.0//PT-BR");

            // Definindo o fuso horário
            sb.AppendLine("BEGIN:VTIMEZONE");
            sb.AppendLine("TZID:America/Sao_Paulo");
            sb.AppendLine("X-LIC-LOCATION:America/Sao_Paulo");
            sb.AppendLine("BEGIN:STANDARD");
            sb.AppendLine("TZOFFSETFROM:-0300");
            sb.AppendLine("TZOFFSETTO:-0300");
            sb.AppendLine("TZNAME:BRT");
            sb.AppendLine("DTSTART:19700101T000000");
            sb.AppendLine("END:STANDARD");
            sb.AppendLine("END:VTIMEZONE");

            // Evento
            sb.AppendLine("BEGIN:VEVENT");
            sb.AppendLine($"UID:{Guid.NewGuid()}");
            sb.AppendLine($"SUMMARY:{request.Titulo}");
            sb.AppendLine($"DESCRIPTION:{request.Descricao}");
            sb.AppendLine($"LOCATION:{request.Local}");
            sb.AppendLine("DTSTART;TZID=America/Sao_Paulo:" + request.Inicio.ToString("yyyyMMdd'T'HHmmss"));
            sb.AppendLine("DTEND;TZID=America/Sao_Paulo:" + request.Fim.ToString("yyyyMMdd'T'HHmmss"));
            sb.AppendLine("END:VEVENT");

            sb.AppendLine("END:VCALENDAR");


            var bytes = Encoding.UTF8.GetBytes(sb.ToString());

            return File(bytes, "text/calendar", "agendamento.ics");
        }

        [HttpPost("gerar-ics-com-lembrete")]
        public IActionResult GerarICSComLembrete([FromBody] AgendamentoLembreteRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sb = new StringBuilder();

            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:-//CaldasTI//GeradorICS 1.0//PT-BR");

            // Fuso horário
            sb.AppendLine("BEGIN:VTIMEZONE");
            sb.AppendLine("TZID:America/Sao_Paulo");
            sb.AppendLine("X-LIC-LOCATION:America/Sao_Paulo");
            sb.AppendLine("BEGIN:STANDARD");
            sb.AppendLine("TZOFFSETFROM:-0300");
            sb.AppendLine("TZOFFSETTO:-0300");
            sb.AppendLine("TZNAME:BRT");
            sb.AppendLine("DTSTART:19700101T000000");
            sb.AppendLine("END:STANDARD");
            sb.AppendLine("END:VTIMEZONE");

            // Evento
            sb.AppendLine("BEGIN:VEVENT");
            sb.AppendLine($"UID:{Guid.NewGuid()}");
            sb.AppendLine($"SUMMARY:{request.Titulo}");
            sb.AppendLine($"DESCRIPTION:{request.Descricao}");
            sb.AppendLine($"LOCATION:{request.Local}");
            sb.AppendLine("DTSTART;TZID=America/Sao_Paulo:" + request.Inicio.ToString("yyyyMMdd'T'HHmmss"));
            sb.AppendLine("DTEND;TZID=America/Sao_Paulo:" + request.Fim.ToString("yyyyMMdd'T'HHmmss"));

            //Lembrete
            sb.AppendLine("BEGIN:VALARM");
            sb.AppendLine($"TRIGGER:-PT{request.MinutosLembrete}M");
            sb.AppendLine("ACTION:DISPLAY");
            sb.AppendLine("DESCRIPTION:Lembrete do evento");
            sb.AppendLine("END:VALARM");

            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/calendar", "agendamento_com_lembrete.ics");
        }

    }
}
