using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak
{
    class CommonBC
    {
        internal static void Sincronyze(object origin, object destiny)
        {
            //Propiedades de la entidad de origen: ( Cliente )
            PropertyInfo[] originProperties = origin.GetType().GetProperties();
            //PropertyInfo[] destinyProperties = destiny.GetType().GetProperties();
            try
            {
                //Recorriendo cada propiedad de la entidad de origen: 
                foreach (PropertyInfo oriProp in originProperties)
                {
                    //Recupera la propiedadad de la entidad de destino:  ( Dato.Cliente.GetProperty(Cliente.Name(Rut))) = Dato.Cliente.Rut;                    
                    PropertyInfo destinyProperty = destiny.GetType().GetProperty(oriProp.Name);
                    
                    try
                    {
                        destinyProperty.SetValue(destiny, oriProp.GetValue(origin, null));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Error: " + ex);
                        continue;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                
            }
        }  
    }
}
