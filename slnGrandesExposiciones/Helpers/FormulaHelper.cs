using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using slnGrandesExposiciones.Models;

namespace slnGrandesExposiciones.Helpers
{
    public class FormulaHelper
    {
        public static List<Formulas> LISTA_FORMULAS { get; set; }
        public static List<Campos> LISTA_CAMPOS { get; set; }

        private static string BuscarFormula(string nombre_formula)
        {
            IEnumerable<Formulas> formula = (from f in FormulaHelper.LISTA_FORMULAS where f.NOMBRE == nombre_formula select f);
            
            if ((formula != null) && (formula.Count() > 0)) { return formula.First().FORMULA; }
            else { return null; }
        }

        public static double ResolverFormula(Exposiciones exp, string nombre_formula)
        {
            //Obtener formula a partir de nombre de campo
            string formula = BuscarFormula(nombre_formula);
            //obtener los alias de la fórmula
            string[] split = formula.Split('[', ']');
            //obtener todos los campos de la tabla exposiciones
            IEnumerable<System.Reflection.PropertyInfo> propiedades = exp.GetType().GetProperties();

            //Con cada alias
            for (int i = 0; i < split.Length; i++)
            {
                if ((split[i].Trim() != "") && (split[i].All(char.IsNumber)))
                {
                    //Reconstruir el alias
                    string id_campo = string.Concat("[", split[i], "]");
                    //Validar el alias
                    IEnumerable<string> campos = (from c in FormulaHelper.LISTA_CAMPOS where ((c.ALIAS == id_campo) && (c.ID_TABLA == 4)) select c.CAMPO);

                    if (campos.Count() > 0)
                    {
                        string nombre_campo = campos.First();

                        //Con cada campo de la tabla exposiciones
                        foreach (System.Reflection.PropertyInfo prop in propiedades)
                        {
                            if (prop.Name == nombre_campo)
                            {
                                string name = prop.Name;
                                object value = prop.GetValue(exp);
                                IEnumerable<Formulas> f_aux = (from f in FormulaHelper.LISTA_FORMULAS where f.NOMBRE == name select f);

                                //Verificar si el campo se trata de una fórmula o un valor
                                if ((f_aux != null) && (f_aux.Count() > 0))
                                {
                                    if ((value != null) && (Convert.ToInt32(value) == 0))
                                    {
                                        //REVISAR SI SE PUEDE USAR EL VALOR CALCULADO PREVIAMENTE DE LA FÓRMULA
                                        split[i] = Convert.ToString(ResolverFormula(exp, f_aux.First().NOMBRE));
                                    }
                                    else
                                    {
                                        split[i] = Convert.ToString(value);
                                    }
                                }
                                else
                                {
                                    split[i] = Convert.ToString(value);
                                }

                                break;
                            }
                        }
                    }
                    else
                    {
                        //Log error

                        return 0d;
                    }
                }

                //Calcular porcentajes
                if (split[i].Contains("%"))
                {
                    string porc = string.Empty;
                    int desde = 0, hasta = 0;

                    hasta = split[i].IndexOf('%');

                    for (int j = (hasta - 1); j > -1; j--)
                    {
                        if (!char.IsNumber(split[i][j]))
                        {
                            desde = (j + 1);

                            break;
                        }
                    }

                    porc = split[i].Substring(desde, (hasta - desde));
                    porc = string.Concat("(", porc, "/100)");

                    if (desde > 0)
                    {
                        porc = string.Concat(split[i].Substring(0, desde), porc);
                    }
                    if ((hasta + 1) < split[i].Length)
                    {
                        porc = string.Concat(porc, split[i].Substring(hasta + 1));
                    }

                    split[i] = porc;
                }
            }

            //Calcular operación
            string calculo = string.Empty;

            split.ToList().ForEach(c => calculo += c);

            if ((calculo = calculo.Trim()) == "")
            {
                calculo = "0";
            }
            else
            {
                //calculo = Convert.ToString(new System.Data.DataTable().Compute(calculo, "")); <--------------------
                calculo = "0";
            }

            return Convert.ToDouble(calculo);
        }
    }
}