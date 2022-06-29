using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace EF.API.Token.Config
{
    public class EventosValidacaoTokenJwt : JwtBearerEvents
    {
        public override Task Challenge(JwtBearerChallengeContext context)
        {
            context.HandleResponse();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 401;

            string mensagem = "Permissão negada! Usuário não autenticado.";
            if (context.AuthenticateFailure != null && !string.IsNullOrEmpty(context.AuthenticateFailure.Message))
                mensagem = context.AuthenticateFailure.Message;

            var retorno = JsonConvert.SerializeObject(new { Mensagem = mensagem });
            return context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(retorno)).AsTask();
        }
    }
}
