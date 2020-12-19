using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace Compiladores_proyecto
{
    public class LR0
    {
        public List<Destado> c = new List<Destado>();
        public Gramatica G = new Gramatica();

        public void set_gramatica()
        {
            Simbolo_Gramatical simbolo_aux;
            List<Simbolo_Gramatical> produccion_amentada = new List<Simbolo_Gramatical>();

            // Se agrega manualmente la produccion aumentada (programa' -> .programa)
            simbolo_aux = new Simbolo_Gramatical("programa'", "NT");
            produccion_amentada.Add(simbolo_aux);
            simbolo_aux = new Simbolo_Gramatical(".", "PUNTO");
            produccion_amentada.Add(simbolo_aux);
            simbolo_aux = new Simbolo_Gramatical("programa", "NT");
            produccion_amentada.Add(simbolo_aux);

            //// Se agrega manualmente la produccion aumentada (programa' -> .programa)
            //simbolo_aux = new Simbolo_Gramatical("S'", "NT");
            //produccion_amentada.Add(simbolo_aux);
            //simbolo_aux = new Simbolo_Gramatical(".", "PUNTO");
            //produccion_amentada.Add(simbolo_aux);
            //simbolo_aux = new Simbolo_Gramatical("S", "NT");
            //produccion_amentada.Add(simbolo_aux);


            G.producciones.Add(produccion_amentada);
            G.set_gramatica_tiny();
        }

        public List<List<Simbolo_Gramatical>> cerradura(List<List<Simbolo_Gramatical>> producciones)
        {
            // Se inicializa J 
            List<List<Simbolo_Gramatical>> J = new List<List<Simbolo_Gramatical>>();
            List<Simbolo_Gramatical> prod_aux = new List<Simbolo_Gramatical>(); // Produccion a la que se le va a agregar el punto
            Simbolo_Gramatical punto = new Simbolo_Gramatical(".", "PUNTO");
            bool band_agrega_j = true;
            // Lo que le haya llegado de ir_a(), lo mete en J
            foreach(List<Simbolo_Gramatical> produc in producciones)
                J.Add(produc);

            // Busca NO TERMINALES despues del punto en cada produccion de J
            for(int i = 0; i < J.Count; i++)
            {
                // Si el ultimo elemento de esa produccion NO es el punto, entonces está en medio el punto
                if (J[i][J[i].Count-1].simbolo != punto.simbolo)
                {
                    // Recorre la produccion pero desde 1 porque produccion[0] es A ( A -> laskdjaklla )
                    for (int tam_prod = 1; tam_prod < J[i].Count; tam_prod++)
                    {
                        // Si ese elemento es .
                        if (J[i][tam_prod].simbolo == punto.simbolo)
                        {
                            // Si el que sigue de ese punto es un NO TERMINAL
                            if(J[i][tam_prod+1].tipo == "NT")
                            {
                                // Se tiene que ir a recorrer G en busca de ese NO TERMINAL y sus producciones
                                foreach(List<Simbolo_Gramatical> produc in G.producciones)
                                {
                                    if (produc[0].simbolo == J[i][tam_prod + 1].simbolo) // Encuentra el NO TERMINAL
                                    {
                                        prod_aux = new List<Simbolo_Gramatical>();
                                        band_agrega_j = true;

                                        // Se le agrega el punto a la produccion
                                        prod_aux.Add(produc[0]);
                                        prod_aux.Add(punto);
                                        for (int k = 1; k < produc.Count; k++)
                                            prod_aux.Add(produc[k]);

                                        // Se recorre de nuevo J para checar que no exista
                                        for(int j = 0; j < J.Count; j++)
                                            if (compara_producciones(J[j], prod_aux)) // La encontró, por lo tanto ya existe en J
                                                band_agrega_j = false;

                                        // Si no existia, la agrega
                                        if (band_agrega_j)
                                            J.Add(prod_aux);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return J;
        }

        public List<List<Simbolo_Gramatical>> ir_a(Destado I, Simbolo_Gramatical X)
        {
            Simbolo_Gramatical punto = new Simbolo_Gramatical(".", "PUNTO");
            List<Simbolo_Gramatical> prod_aux = new List<Simbolo_Gramatical>(); // Produccion a la que se le va a desplazar el punto
            List<List<Simbolo_Gramatical>> list_prod_aux = new List<List<Simbolo_Gramatical>>(); // Lista de las producciones a las que se les va a desplazar el punto
            bool band_recorre_punto = false; // Bandera usada para saber si se tiene que recorrer el punto o no
                                                                                
            // Se busca, en todas las producciones de I, A -> a.X
            foreach(List<Simbolo_Gramatical> produccion in I.conjunto_lr0)
            {
                band_recorre_punto = false;
                prod_aux = new List<Simbolo_Gramatical>();

                if (produccion[produccion.Count - 1].simbolo != punto.simbolo) // Si el punto está hasta el final, ya mejor ni hace nada
                {
                    // Se recorre la produccion en busca del punto
                    // Se empieza en 1 porque produccion[0] es A (A -> a.X)
                    for (int i = 1; i < produccion.Count; i++)
                    {
                        if (produccion[i].simbolo == punto.simbolo) // Encuentra el punto
                        {
                            if (produccion[i + 1].simbolo == X.simbolo) // Encuentra el simbolo gramatical buscado despues del punto
                            {
                                // Hay que desplazar el punto un lugar a la derecha
                                band_recorre_punto = true;
                                break;
                            }
                        }
                    }
                }
                
                if (band_recorre_punto)
                {
                    // Recorre el punto
                    for (int i = 0; i < produccion.Count; i++)
                    {
                        if(produccion[i].simbolo == punto.simbolo)
                        {
                            prod_aux.Add(produccion[i+1]);
                            prod_aux.Add(punto);
                            i++;
                        }
                        else
                            prod_aux.Add(produccion[i]);
                    }

                    // Añade a la lista la produccion ya con el punto recorrido
                    list_prod_aux.Add(prod_aux);
                }
            }
            
            // Si si encontró para recorrer el punto, regresa la cerradura de eso.
            if(list_prod_aux.Count == 0)
                return null;

            return cerradura(list_prod_aux);
        }

        bool compara_producciones(List<Simbolo_Gramatical> a, List<Simbolo_Gramatical> b)
        {
            bool iguales = true;

            // Si nisiquiera tienen el mismo tamaño, pues desde ahi no son ifuales
            if (a.Count != b.Count)
                iguales = false;

            // Si si tienen el mismo tamaño, iguales se queda en true asi que se va a checar simbolo por simbolo
            if (iguales)
            {
                // Se recorre con el tamaño de a porque no importa si es de a o de b, ambas tienen el mismo tamaño
                for(int i = 0; i < a.Count; i++)
                {
                    if (a[i].simbolo != b[i].simbolo) // Con que un simbolo ya no sea igual al otro, entonces ya no son iguales
                    {
                        iguales = false;
                        break;
                    }
                }
            }

            return iguales;
        }

        public string analisis_accion_relga1(List<Simbolo_Gramatical> elemento, Destado est, Simbolo_Gramatical s, List<Transicion> transiciones) 
        {
            // Regla 1 ( . antes de un terminal )
            string celda = "";

            if (elemento[elemento.Count-1].simbolo != ".") // Si el punto está al final, no hace nada
            {
                for (int i = 1; i < elemento.Count; i++) // Recorre simbolo por simbolo del elemento, empieza en 1 porque 0 es A (A -> LASKDJADHF)
                {
                    if (elemento[i].simbolo == ".") // Encuentra el punto
                    {
                        if (elemento[i+1].simbolo == s.simbolo) // Encuentra el simbolo terminal despues del punto
                        {
                            // De las transiciones ya creadas, se busca el ir_a de est con s
                            foreach(Transicion t in transiciones)
                            {
                                //MessageBox.Show("["+t.destado_origen.id.Replace("\n", "")+ "]" + "  - [" + s.simbolo + "] | [" + t.simbolo_lr0 + "] >  " + "[" + est.id.Replace("\n", "") + "]");
                                if(t.destado_origen.id.Replace("\n", "") == est.id.Replace("\n", "") && t.simbolo_lr0 == s.simbolo) // Encontro el destado del que sale
                                {
                                    //MessageBox.Show("Entra con: [" + t.destado_origen.id.Replace("\n", "") + "]" + "  - [" + s.simbolo + "] | [" + t.simbolo_lr0 + "] >  " + "[" + est.id.Replace("\n", "") + "]");
                                    celda = "d" + t.destado_destino.id.Trim('I');
                                    //MessageBox.Show(celda);
                                    return celda;
                                }
                            }
                        }
                    }
                }
            }

            return celda;
        }

        public List<List<string>> analisis_accion_relga2(List<Simbolo_Gramatical> elemento) 
        {
            // Regresa una lista de lista de strings porque:
            //    Una lista de strings es para una sola celda, tiene el formato:
            //      [0] es el siguiente que le corresponde
            //      [1] es el r(n)
            //    Siempre va a ser de tamaño 2 solamente entonces queda ACCION[estado, [0]] = [1]

            // Regla 2 ( . al final menos la aumentada )
            List<List<string>> celdas = new List<List<string>>();
            List<string> celda = new List<string>();
            int num_prod = -1;

            if (elemento[elemento.Count - 1].simbolo == ".") // Si el punto no está al final, no hace nada
            {
                // Se tienen que obtener los siguientes de A (A -> laksdjlaksj) del elemento
                foreach(Simbolo_Gramatical noterminal in G.noterminales) // Recorre todos los no terminales para encontrar A
                {
                    if(noterminal.simbolo == elemento[0].simbolo) // Encuentra el no terminal A
                    {
                        // Para sacar el numero de produccion, se le quita momentaneamente el . al elemento y lo compara con las producciones
                        num_prod = obten_num_produccion(quita_punto_elemento(elemento));

                        // Por cada cosa en los siguientes de A
                        foreach (string sig in noterminal.siguientes)
                        {
                            celda = new List<string>();
                            celda.Add(sig);
                            celda.Add("r" + num_prod.ToString());
                            celdas.Add(celda);
                        }
                    }
                }
            }

            return celdas;
        }

        public int obten_num_produccion(List<Simbolo_Gramatical> p1)
        {
            bool band = true;
            int k = -1;

            for(int j = 1; j < G.producciones.Count; j++) // La primera no la checa porque es la aumentada
            {
                // Primero checa si minimo son del mismo tamaño
                if (p1.Count == G.producciones[j].Count)
                {
                    band = true;
                    // Recorre elemento por elemento a ver si es igual a p1
                    for (int i = 0; i < p1.Count; i++)
                        if (G.producciones[j][i].simbolo != p1[i].simbolo) // Con 1 que no sea igual, entonces ya fue
                            band = false;

                    // Si llega aqui con la bandera en true, significa que si son iguales
                    if (band)
                    {
                        k = j;
                        return k;
                    }
                       
                }
                else
                    band = false;
            }

            return k;
        }

        public List<Simbolo_Gramatical> quita_punto_elemento(List<Simbolo_Gramatical> con_punto)
        {
            List<Simbolo_Gramatical> sin_punto = new List<Simbolo_Gramatical>();

            // Añade todos los elementos menos el punto
            foreach(Simbolo_Gramatical simbolo in con_punto)
                if(simbolo.simbolo != ".")
                    sin_punto.Add(simbolo);

            return sin_punto;
        }

        public string analisis_accion_relga3(List<Simbolo_Gramatical> elemento) 
        {
            // Regla 3 ( . al final en la aumentada )
            string celda = "";
            // Se tiene ya la produccion aumentada pero con el punto al inicio (es la primera produccion de la lista de producciones)
            if (elemento.Count == 3) // La produccion aumentada solo tiene 3 valores unicamente, noterminal, noterminal' y punto
            {
                if (elemento[elemento.Count - 1].simbolo == ".") // Si el punto no está al final, no hace nada
                {
                    // Compara manualmente y a golpe el elemento
                    if (elemento[0].simbolo == G.producciones[0][0].simbolo && elemento[1].simbolo == G.producciones[0][2].simbolo)
                    {
                        // Si es la produccion aumentada con el punto al final
                        celda = "ac";
                    }
                }
            }

            return celda;
        }

        public bool compara_ListaProducciones(List<List<Simbolo_Gramatical>> a, List<List<Simbolo_Gramatical>> b)
        {
            bool iguales = true;
            List<string> a_s = new List<string>();
            string b_s = "";
            int contador = 0; // Contador con el que se va a checar que los elementos de a sean iguales a los de b

            // Si nisiquiera tienen el mismo tamaño, pues desde ahi no son ifuales
            if (a.Count != b.Count)
                iguales = false;

            // Si al menos el tamaño es el mismo, entonecs PUEDEN ser iguales
            if (iguales)
            {
                // Para evitar checar lista de listas, porque es demasiado por el ausnto de que pueden estár en desorden, 
                // se "castean" las listas de listas de simbolos gramaticales, a lista de strings para hacer directo el chequeo
                // a se hace una lista de strings y b se hace una string completa
                a_s = transoforma_a_strings(a);
                b_s = transoforma_a_string(b);

                // Se checa string por string de a, que exista en b, si si existe, se incremente el contador y
                // si al final el contador es igual al count de a, significa que todas y cada una de las strings
                // de a se encontraron en b, lo que significa que a y b son iguales.
                foreach (string s in a_s)
                    if (b_s.Contains(s))
                        contador++;

                // Significa que no se encontraron todas las producciones de a en b, por lo tanto, no son iguales
                if (contador != a_s.Count)
                    iguales = false;
            }

            return iguales;
        }

        public List<string> transoforma_a_strings(List<List<Simbolo_Gramatical>> a)
        {
            List<string> l = new List<string>();
            string produccion = "";

            // Recorre una produccion de la lista de producciones
            foreach (List<Simbolo_Gramatical> prod in a)
            {
                produccion = "";
                // Recorre todos los elementos de esa produccion y los concatena
                foreach (Simbolo_Gramatical sim in prod)
                    produccion += sim.simbolo;

                l.Add(produccion);
            }

            return l;
        }

        public string transoforma_a_string(List<List<Simbolo_Gramatical>> a)
        {
            string produccion = "";

            // Recorre una produccion de la lista de producciones
            foreach (List<Simbolo_Gramatical> prod in a)
            {
                // Recorre todos los elementos de esa produccion y los concatena
                foreach (Simbolo_Gramatical sim in prod)
                    produccion += sim.simbolo;
            }

            return produccion;
        }
    }
}
