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
        public List<string> primeros = new List<string>();
        public List<string> siguientes = new List<string>();
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
    }
}
