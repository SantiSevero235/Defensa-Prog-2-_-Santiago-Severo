using Telegram.Bot.Types;
using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "Remover".
    /// </summary>
    public class RemoverUsuarioHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BaseHandler"/>. Esta clase procesa el mensaje "Remover".
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public RemoverUsuarioHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/Remover"};
        }

        /// <summary>
        /// Procesa el mensaje "Remover" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="mensaje">El mensaje a procesar.</param>
        /// <param name="respuesta">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message mensaje, out string respuesta)
        {
            try
            {
                respuesta = string.Empty;
                if (this.CanHandle(mensaje))
                {
                    long IDdeljugador = mensaje.Chat.Id;
                    AlmacenamientoUsuario almacenamiento = AlmacenamientoUsuario.Instance();
                    int jugador = almacenamiento.ConversorIDaNum(IDdeljugador);
                    Planificador.Remover(jugador);
                    respuesta += "Su usuario ha sido removido\nSi desea volver a ingresar debe registrarse\n\nUse /Registrar para volver a registrarse";
                    UsersHistory estados = UsersHistory.Instance();
                    estados.RetrocederEstados(IDdeljugador,1);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                long IDdeljugador = mensaje.Chat.Id;
                UsersHistory estados = UsersHistory.Instance();
                respuesta = string.Empty;
                respuesta = "Ha habido un error. Intente de nuevo \n";
                estados.ReiniciarEstados(IDdeljugador);
                return true;
            }
        }
    }
}
