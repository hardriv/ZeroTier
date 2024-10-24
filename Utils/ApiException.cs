using System;

namespace ZeroTier.Utils
{
    public class ApiException : Exception
    {
        public int StatusCode { get; private set; }

        public ApiException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public ApiException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(int statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public override string Message
        {
            get
            {
                // Retourne le message correspondant au code d'erreur HTTP
                return StatusCode switch
                {
                    401 => "Erreur : Accès non autorisé (401). Veuillez vérifier votre token API.",
                    403 => "Erreur : Accès refusé (403). Vous n'avez pas les droits nécessaires.",
                    404 => "Erreur : Ressource non trouvée (404).",
                    >= 500 and < 600 => "Erreur serveurs, veuillez essayer de nouveau plus tard.",
                    _ => $"Erreur inattendue (Code: {StatusCode})."
                };
            }
        }
    }

}
