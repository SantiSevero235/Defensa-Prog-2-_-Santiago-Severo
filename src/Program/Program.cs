﻿//--------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using ClassLibrary;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace ConsoleApplication
{
    /// <summary>
    /// Programa de consola de demostración.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Punto de entrada al programa principal.
        /// </summary>
        public static void Main()
        {
            /*Jugador jugador1 = new Jugador("Jugador1", 98, "tonto");
            Jugador jugador2 = new Jugador("Jugador2",87,"perro");

            //jugador1.PartidaAmistosa(0, jugador2.NumeroDeJugador, 7);
            jugador1.BuscarPartida(0,7);
            jugador2.BuscarPartida(0,7);

            Console.WriteLine(jugador1.PosicionarBarcos("A1","A6"));
            Console.WriteLine(jugador1.PosicionarBarcos("B1","B6"));
            Console.WriteLine(jugador2.PosicionarBarcos("E1","E6"));
            Console.WriteLine(jugador2.PosicionarBarcos("F1","F6"));
            
            int i = 1;
            while(i <= 6)
            {
                Console.WriteLine(jugador1.Atacar($"G{i}"));
                Console.WriteLine(jugador2.Atacar($"A{i}"));
                i+=1;
            }
            i = 1;
            while(i < 6)
            {
                Console.WriteLine(jugador1.Atacar($"C{i}"));
                Console.WriteLine(jugador2.Atacar($"B{i}"));
                i+=1;
            }
            //SE VEN SOLO TRES ATAQUES DE LOS 4
            jugador1.VisualizarTableros();
            jugador2.VisualizarTableros();

            //Console.WriteLine(jugador1.Atacar("C6"));
            //Console.WriteLine(jugador2.Atacar("B6")); //Termino la partida
            jugador1.VisualizarTableros();
            jugador2.VisualizarTableros();*/

            BaseHandler ConfirmarBusqueda = new ConfirmarBusquedaHandler(null);
            BaseHandler BuscarPartidaAmistosa = new BuscarPartidaAmistosaHandler(ConfirmarBusqueda);
            BaseHandler BuscarPartida = new BuscarPartidaHandler(BuscarPartidaAmistosa);
            BaseHandler VisualizarTableros = new VisualizarTablerosHandler(BuscarPartida);
            BaseHandler VerHistorialPersonal = new VerHistorialPersonalHandler(VisualizarTableros);
            BaseHandler VerHistorial = new VerHistorialHandler(VerHistorialPersonal);
            BaseHandler VerRanking = new VerRankingHandler(VerHistorial);
            BaseHandler VerPerfil = new VerPerfilHandler(VerRanking);
            BaseHandler remover = new RemoverHandler(VerPerfil);
            BaseHandler menu = new MenuHandler(remover);
            BaseHandler inicioSesion = new InicioSesionHandler(menu);
            BaseHandler registrar = new RegistrarHandler(menu);
            IHandler comenzar = new ComenzarHandler(registrar);
            Message mensaje = new Message();
            string respuesta;

            List<string> Guardado = new List<string>();

            Console.WriteLine("escriba un comando o 'salir': ");
            Console.Write("> ");

            while(true)
            {
                mensaje.Text = Console.ReadLine();
                if (mensaje.Text.Equals("salir", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Adios");
                    return;
                }
                IHandler resultado = comenzar.Handle(mensaje, out respuesta);
                if (resultado == null)
                {
                    Console.WriteLine("No entiendo");
                    Console.Write("> ");
                }
                else
                {
                    Console.WriteLine(respuesta);
                    Console.Write("> ");
                }
            }
        }
    }
}
