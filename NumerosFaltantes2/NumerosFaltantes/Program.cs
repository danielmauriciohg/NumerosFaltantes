using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumerosFaltantes
{
    public class Program
    {
        /// <summary>
        /// Convierte la entrada de String a Lista
        /// </summary>
        /// <param name="datos">String de valores </param>
        /// <returns></returns>
        public static string[] ConvertirLista(string datos)
        {
            string[] listaEnteros; ;
            Char separador = ' ';
            listaEnteros = datos.Split(separador);
            return listaEnteros;

        }

        /// <summary>
        /// Valida que la cantidad de valores ingresadas
        /// sea el mismo que la cantidad de elementos que el usuario indica que va a tener la lista
        /// </summary>
        /// <param name="listaValidar">Valores de la Lista a Validar</param>
        /// <param name="posiciones">Longitud indicada por el usuario</param>
        /// <returns></returns>
        public static bool ValidarLista(string[] listaValidar, int posiciones)
        {
            var tmpValidacion = true;
            int numero;
            //Validar la diferencia entre el mayor y el menor
            foreach(string valorIgresado in listaValidar)
            {
                if(!int.TryParse(valorIgresado,out numero))
                {
                    Console.WriteLine("No se pudo convertir a número el valor: "+ valorIgresado);
                    tmpValidacion = false;
                }
            }
            if (listaValidar.Length == posiciones && tmpValidacion)
                return true;
            else
            {
                return false;
            }
        }

        /// <summary>
        ///Validad la cantidad de posiciones que puede tener la lista
        /// </summary>
        /// <param name="longitudLista"> cantidad de elemento ingresada por el usuario</param>
        /// <returns></returns>
        public static bool ValidarLongitudLista(int longitudLista)
        {
            var limiteMax = 2 * Math.Pow(10, 5);
            if (longitudLista >= 1 && longitudLista <= limiteMax)
                return true;
            return false;
        }

        


        /// <summary>
        /// Valida condiciones de los valores de la lista B
        /// </summary>
        /// <param name="listB">Lista de enteros </param>
        /// <param name="posiciones">entero de posiciones de la listaB</param>
        /// <returns></returns>
        public static bool ValidarListaB(string[] listB, int posiciones)
        {
            List<string> listaDatosentrada = listB.ToList();
            List<int> listaNumerosEnteros = listaDatosentrada.ConvertAll(s => Int32.Parse(s));
            var limiteMaxValores = Math.Pow(10, 4);
            int diferencia = 0;
           
            listaNumerosEnteros.Sort();
           
            diferencia = listaNumerosEnteros[posiciones - 1] - listaNumerosEnteros[0];
            if(diferencia > 100)
            {
                Console.WriteLine("La direncia entre el Minimo y maximo valor de la lista no puede ser superior a 100 ");
                return false;
            }
            int resultado = (from dato in listaNumerosEnteros
                             where dato > limiteMaxValores || dato < 1
                             select dato).Count();
            if(resultado > 0)
            {
                Console.WriteLine("uno de los valores supera los topes permitidos "+"1<=valor<="+ limiteMaxValores);
                return false;
            }
                
            return  true;
        }

        /// <summary>
        ///realiza la busqueda de los numeros faltantes en Lista A
        /// </summary>
        /// <param name="listaA">ListaA de entrada</param>
        /// <param name="listaB">Listab de entrada</param>
        /// <returns></returns>
        public static List<int> EncontrarFaltantes(string[] listaA, string[] listaB)
        {
            List<int> ListaValoresFaltantes = new List<int>();
            IEnumerable<string> ListaBNoRepetidos = listaB.Distinct();
            int cantidadA = 0;
            int cantidadB = 0;
            foreach (string valorDato in ListaBNoRepetidos)
            {
                cantidadB = (from datoB in listaB
                             where datoB == valorDato
                             select datoB).Count();

                cantidadA = (from datoA in listaA
                             where datoA == valorDato
                             select datoA).Count();
                if (cantidadA < cantidadB)
                {
                    ListaValoresFaltantes.Add(Int32.Parse(valorDato));
                   // faltantes = faltantes + valorDato + " ";
                }
                
            }

            ListaValoresFaltantes.Sort();
            return ListaValoresFaltantes;
        }


        public static void Main(string[] args)
        {
            string longitudListaA = "";
            string logitudListaB = "";
            int longA = 0;
            int longB = 0;
            bool validarValorLongitudA = false;
            bool validarValorLongitudB = false;
            bool validaListaA = false;
            bool validaListaB = false;
            bool validaListaB2 = false;
            string listaAInput = "";
            string listaBInput = "";
            string[] listaA;
            string[] listaB;
            List<int> faltantes = new List<int>();
            //validar la longitud de la lista A
            while (!validarValorLongitudA)
            {
                Console.WriteLine("Escriba la longitud de ListaA(n):");
                longitudListaA = Console.ReadLine();
                longA = Convert.ToInt32(longitudListaA);
                validarValorLongitudA = ValidarLongitudLista(longA);
            }
            listaA = new string[longA];
            //Valida los valores de lista A
            while (!validaListaA)
            {
                Console.WriteLine("Escriba los Valores de Lista A:");
                listaAInput = Console.ReadLine();
                listaA = ConvertirLista(listaAInput);
                validaListaA = ValidarLista(listaA, longA);
                
            }
            //validar la longitud de la lista B
            while (!validarValorLongitudB)
            {
                Console.WriteLine("Escriba la longitud de ListaB(m):");
                logitudListaB = Console.ReadLine();
                longB = Convert.ToInt32(logitudListaB);
                validarValorLongitudB = ValidarLongitudLista(longB);


                if (longB < longA)
                {
                    Console.WriteLine("La Longitud de Lista B debe ser mayor a Lista A");
                }

            }
            
            listaB = new string[longB];
            //Valida los valores de lista B
            while (!validaListaB || !validaListaB2)
            {
                Console.WriteLine("Escriba los Valores de Lista B:");
                listaBInput = Console.ReadLine();
                listaB = ConvertirLista(listaBInput);
                validaListaB = ValidarLista(listaB, longB);
                if (validaListaB)
                {
                    validaListaB2 = ValidarListaB(listaB, longB);
                }
            
            }


            faltantes = EncontrarFaltantes(listaA, listaB);
            Console.WriteLine("Numero Faltantes son:");
            foreach (var nroFaltante in faltantes)
            {
                Console.Write (nroFaltante + " ");
            }
                
            //Console.WriteLine("Numero Faltantes:" + faltantes);
            Console.ReadLine();
            
        }
    }
}
