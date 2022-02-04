using System.Threading.Tasks;

namespace Pre_Aceleracion_Julieta_Sosa.Interfaces
{
    public interface IMailService
    {
        Task SendMail(string email, string subject, string htmlContent);
    }
}
