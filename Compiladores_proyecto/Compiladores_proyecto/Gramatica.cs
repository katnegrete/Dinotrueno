using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_proyecto
{
    public class Gramatica
    {
        public List<List<Simbolo_Gramatical>> producciones = new List<List<Simbolo_Gramatical>>();
        public List<Simbolo_Gramatical> terminales = new List<Simbolo_Gramatical>();
        public List<Simbolo_Gramatical> noterminales = new List<Simbolo_Gramatical>();

        public void set_gramatica_tiny()
        {
            List<Simbolo_Gramatical> lista_aux = new List<Simbolo_Gramatical>();
            Simbolo_Gramatical aux = new Simbolo_Gramatical();

            // PRODUCCIONES:


            // Gramatica meh de P

            //aux = new Simbolo_Gramatical("P", "NT");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("a", "T");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("P", "NT");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("a", "T");
            //lista_aux.Add(aux);
            //producciones.Add(lista_aux);
            //lista_aux = new List<Simbolo_Gramatical>();

            //aux = new Simbolo_Gramatical("P", "NT");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("b", "T");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("P", "NT");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("b", "T");
            //lista_aux.Add(aux);
            //producciones.Add(lista_aux);
            //lista_aux = new List<Simbolo_Gramatical>();

            //aux = new Simbolo_Gramatical("P", "NT");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("c", "T");
            //lista_aux.Add(aux);
            //producciones.Add(lista_aux);
            //lista_aux = new List<Simbolo_Gramatical>();

            //aux = new Simbolo_Gramatical("P", "NT");
            //aux.primeros.Add("a");
            //aux.primeros.Add("b");
            //aux.primeros.Add("c");
            //aux.siguientes.Add("$");
            //aux.siguientes.Add("a");
            //aux.siguientes.Add("b");
            //noterminales.Add(aux);

            //aux = new Simbolo_Gramatical("a", "T");
            //aux.primeros.Add("a");
            //terminales.Add(aux);
            //aux = new Simbolo_Gramatical("b", "T");
            //aux.primeros.Add("b");
            //terminales.Add(aux);
            //aux = new Simbolo_Gramatical("c", "T");
            //aux.primeros.Add("c");
            //terminales.Add(aux);






            // Gramatica meh de S

            //aux = new Simbolo_Gramatical("S", "NT");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("(", "T");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("L", "NT");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical(")", "T");
            //lista_aux.Add(aux);
            //producciones.Add(lista_aux);
            //lista_aux = new List<Simbolo_Gramatical>();

            //aux = new Simbolo_Gramatical("S", "NT");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("id", "T");
            //lista_aux.Add(aux);
            //producciones.Add(lista_aux);
            //lista_aux = new List<Simbolo_Gramatical>();

            //aux = new Simbolo_Gramatical("L", "NT");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("L", "NT");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical(",", "T");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("S", "NT");
            //lista_aux.Add(aux);
            //producciones.Add(lista_aux);
            //lista_aux = new List<Simbolo_Gramatical>();

            //aux = new Simbolo_Gramatical("L", "NT");
            //lista_aux.Add(aux);
            //aux = new Simbolo_Gramatical("S", "NT");
            //lista_aux.Add(aux);
            //producciones.Add(lista_aux);
            //lista_aux = new List<Simbolo_Gramatical>();

            //aux = new Simbolo_Gramatical("S", "NT");
            //aux.primeros.Add(",");
            //aux.primeros.Add("id");
            //aux.siguientes.Add("$");
            //aux.siguientes.Add(")");
            //aux.siguientes.Add(",");
            //noterminales.Add(aux);

            //aux = new Simbolo_Gramatical("L", "NT");
            //aux.primeros.Add(",");
            //aux.primeros.Add("id");
            //aux.siguientes.Add(")");
            //aux.siguientes.Add(",");
            //noterminales.Add(aux);

            //aux = new Simbolo_Gramatical("(", "T");
            //aux.primeros.Add("(");
            //terminales.Add(aux);
            //aux = new Simbolo_Gramatical(")", "T");
            //aux.primeros.Add(")");
            //terminales.Add(aux);

            //aux = new Simbolo_Gramatical(",", "T");
            //aux.primeros.Add(",");
            //terminales.Add(aux);
            //aux = new Simbolo_Gramatical("id", "T");
            //aux.primeros.Add("id");
            //terminales.Add(aux);








            //TINY

            // programa -> secuencia-sent
            aux = new Simbolo_Gramatical("programa", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("secuencia-sent", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // secuencia-sent -> secuencia-sent ; sentencia
            aux = new Simbolo_Gramatical("secuencia-sent", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("secuencia-sent", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical(";", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("sentencia", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // secuencia-sent -> sentencia
            aux = new Simbolo_Gramatical("secuencia-sent", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("sentencia", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // sentencia -> sent-if
            aux = new Simbolo_Gramatical("sentencia", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("sent-if", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // sentencia -> sent-repeat
            aux = new Simbolo_Gramatical("sentencia", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("sent-repeat", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // sentencia -> sent - assign 
            aux = new Simbolo_Gramatical("sentencia", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("sent-assign", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // sentencia -> sent - read
            aux = new Simbolo_Gramatical("sentencia", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("sent-read", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // sentencia -> sent - write
            aux = new Simbolo_Gramatical("sentencia", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("sent-write", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // sent-if -> if exp then secuencia-sent end 
            aux = new Simbolo_Gramatical("sent-if", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("if", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("exp", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("then", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("secuencia-sent", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("end", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // sent-if -> if exp then secuencia-sent else secuencia-sent end
            aux = new Simbolo_Gramatical("sent-if", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("if", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("exp", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("then", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("secuencia-sent", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("else", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("secuencia-sent", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("end", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // sent-repeat -> repeat secuencia-sent until exp
            aux = new Simbolo_Gramatical("sent-repeat", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("repeat", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("secuencia-sent", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("until", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("exp", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // sent-assign -> identificador := exp
            aux = new Simbolo_Gramatical("sent-assign", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("identificador", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical(":=", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("exp", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // sent-read -> read identificador
            aux = new Simbolo_Gramatical("sent-read", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("read", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("identificador", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // sent-write -> write exp
            aux = new Simbolo_Gramatical("sent-write", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("write", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("exp", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // exp -> exp-simple op-comp exp-simple
            aux = new Simbolo_Gramatical("exp", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("exp-simple", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("op-comp", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("exp-simple", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // exp -> exp-simple
            aux = new Simbolo_Gramatical("exp", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("exp-simple", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // op-comp -> <
            aux = new Simbolo_Gramatical("op-comp", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("<", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // op-comp -> > 
            aux = new Simbolo_Gramatical("op-comp", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical(">", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // op-comp -> =
            aux = new Simbolo_Gramatical("op-comp", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("=", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // exp-simple -> exp-simple opsuma term
            aux = new Simbolo_Gramatical("exp-simple", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("exp-simple", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("opsuma", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("term", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // exp-simple -> term
            aux = new Simbolo_Gramatical("exp-simple", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("term", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // opsuma -> +
            aux = new Simbolo_Gramatical("opsuma", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("+", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // opsuma -> -
            aux = new Simbolo_Gramatical("opsuma", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("-", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // term -> term opmult factor
            aux = new Simbolo_Gramatical("term", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("term", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("opmult", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("factor", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // term -> factor
            aux = new Simbolo_Gramatical("term", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("factor", "NT");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // opmult -> *
            aux = new Simbolo_Gramatical("opmult", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("*", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // opmult -> /
            aux = new Simbolo_Gramatical("opmult", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("/", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // factor -> ( exp )
            aux = new Simbolo_Gramatical("factor", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("(", "T");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("exp", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical(")", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // factor -> numero
            aux = new Simbolo_Gramatical("factor", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("numero", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();

            // factor -> identificador
            aux = new Simbolo_Gramatical("factor", "NT");
            lista_aux.Add(aux);
            aux = new Simbolo_Gramatical("identificador", "T");
            lista_aux.Add(aux);
            producciones.Add(lista_aux);
            lista_aux = new List<Simbolo_Gramatical>();





            // TERMINALES Y NO TERMINALES

            aux = new Simbolo_Gramatical("programa", "NT");
            aux.primeros.Add("if");
            aux.primeros.Add("repeat");
            aux.primeros.Add("identificador");
            aux.primeros.Add("read");
            aux.primeros.Add("write");
            aux.siguientes.Add("$");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("secuencia-sent", "NT");
            aux.primeros.Add("if");
            aux.primeros.Add("repeat");
            aux.primeros.Add("identificador");
            aux.primeros.Add("read");
            aux.primeros.Add("write");
            aux.siguientes.Add("$");
            aux.siguientes.Add(";");
            aux.siguientes.Add("end");
            aux.siguientes.Add("else");
            aux.siguientes.Add("until");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("sentencia", "NT");
            aux.primeros.Add("if");
            aux.primeros.Add("repeat");
            aux.primeros.Add("identificador");
            aux.primeros.Add("read");
            aux.primeros.Add("write");
            aux.siguientes.Add("$");
            aux.siguientes.Add(";");
            aux.siguientes.Add("end");
            aux.siguientes.Add("else");
            aux.siguientes.Add("until");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("sent-if", "NT");
            aux.primeros.Add("if");
            aux.siguientes.Add("$");
            aux.siguientes.Add(";");
            aux.siguientes.Add("end");
            aux.siguientes.Add("else");
            aux.siguientes.Add("until");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("sent-repeat", "NT");
            aux.primeros.Add("repeat");
            aux.siguientes.Add("$");
            aux.siguientes.Add(";");
            aux.siguientes.Add("end");
            aux.siguientes.Add("else");
            aux.siguientes.Add("until");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("sent-assign", "NT");
            aux.primeros.Add("identificador");
            aux.siguientes.Add("$");
            aux.siguientes.Add(";");
            aux.siguientes.Add("end");
            aux.siguientes.Add("else");
            aux.siguientes.Add("until");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("sent-read", "NT");
            aux.primeros.Add("read");
            aux.siguientes.Add("$");
            aux.siguientes.Add(";");
            aux.siguientes.Add("end");
            aux.siguientes.Add("else");
            aux.siguientes.Add("until");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("sent-write", "NT");
            aux.primeros.Add("write");
            aux.siguientes.Add("$");
            aux.siguientes.Add(";");
            aux.siguientes.Add("end");
            aux.siguientes.Add("else");
            aux.siguientes.Add("until");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("exp", "NT");
            aux.primeros.Add("(");
            aux.primeros.Add("numero");
            aux.primeros.Add("identificador");
            aux.siguientes.Add("then");
            aux.siguientes.Add("$");
            aux.siguientes.Add(";");
            aux.siguientes.Add("end");
            aux.siguientes.Add("else");
            aux.siguientes.Add("until");
            aux.siguientes.Add(")");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("op-comp", "NT");
            aux.primeros.Add("<");
            aux.primeros.Add(">");
            aux.primeros.Add("=");
            aux.siguientes.Add("(");
            aux.siguientes.Add("numero");
            aux.siguientes.Add("identificador");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("exp-simple", "NT");
            aux.primeros.Add("(");
            aux.primeros.Add("numero");
            aux.primeros.Add("identificador");
            aux.siguientes.Add("then");
            aux.siguientes.Add("$");
            aux.siguientes.Add(";");
            aux.siguientes.Add("end");
            aux.siguientes.Add("else");
            aux.siguientes.Add("until");
            aux.siguientes.Add(")");
            aux.siguientes.Add("<");
            aux.siguientes.Add(">");
            aux.siguientes.Add("=");
            aux.siguientes.Add("+");
            aux.siguientes.Add("-");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("opsuma", "NT");
            aux.primeros.Add("+");
            aux.primeros.Add("-");
            aux.siguientes.Add("(");
            aux.siguientes.Add("numero");
            aux.siguientes.Add("identificador");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("term", "NT");
            aux.primeros.Add("(");
            aux.primeros.Add("numero");
            aux.primeros.Add("identificador");
            aux.siguientes.Add("<");
            aux.siguientes.Add(">");
            aux.siguientes.Add("=");
            aux.siguientes.Add("then");
            aux.siguientes.Add("$");
            aux.siguientes.Add(";");
            aux.siguientes.Add("end");
            aux.siguientes.Add("else");
            aux.siguientes.Add("until");
            aux.siguientes.Add(")");
            aux.siguientes.Add("+");
            aux.siguientes.Add("-");
            aux.siguientes.Add("*");
            aux.siguientes.Add("/");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("opmult", "NT");
            aux.primeros.Add("*");
            aux.primeros.Add("/");
            aux.siguientes.Add("(");
            aux.siguientes.Add("numero");
            aux.siguientes.Add("identificador");
            noterminales.Add(aux);
            aux = new Simbolo_Gramatical("factor", "NT");
            aux.primeros.Add("(");
            aux.primeros.Add("numero");
            aux.primeros.Add("identificador");
            aux.siguientes.Add("<");
            aux.siguientes.Add(">");
            aux.siguientes.Add("=");
            aux.siguientes.Add("then");
            aux.siguientes.Add("$");
            aux.siguientes.Add(";");
            aux.siguientes.Add("end");
            aux.siguientes.Add("else");
            aux.siguientes.Add("until");
            aux.siguientes.Add(")");
            aux.siguientes.Add("+");
            aux.siguientes.Add("-");
            aux.siguientes.Add("*");
            aux.siguientes.Add("/");
            noterminales.Add(aux);



            aux = new Simbolo_Gramatical(";", "T");
            aux.primeros.Add(";");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("if", "T");
            aux.primeros.Add("if");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("then", "T");
            aux.primeros.Add("then");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("end", "T");
            aux.primeros.Add("end");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("else", "T");
            aux.primeros.Add("else");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("repeat", "T");
            aux.primeros.Add("repeat");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("until", "T");
            aux.primeros.Add("until");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("identificador", "T");
            aux.primeros.Add("identificador");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical(":=", "T");
            aux.primeros.Add(":=");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("read", "T");
            aux.primeros.Add("read");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("write", "T");
            aux.primeros.Add("write");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("<", "T");
            aux.primeros.Add("<");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical(">", "T");
            aux.primeros.Add(">");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("=", "T");
            aux.primeros.Add("=");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("+", "T");
            aux.primeros.Add("+");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("-", "T");
            aux.primeros.Add("-");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("*", "T");
            aux.primeros.Add("*");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("/", "T");
            aux.primeros.Add("/");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("(", "T");
            aux.primeros.Add("(");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical(")", "T");
            aux.primeros.Add(")");
            terminales.Add(aux);
            aux = new Simbolo_Gramatical("numero", "T");
            aux.primeros.Add("numero");
            terminales.Add(aux);
        }
    }
}
