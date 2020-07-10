using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBreak
{
    class FactoryProducer
    {
        //Preparacion Factory
        public AbstractFactory getFactory(string factory)
        {
            if (string.IsNullOrEmpty(factory))
            {
                return null;
            }
            if (factory.Equals("CoffeBreak"))
            {
                return new CoffeBreakFactory();
            }
            if (factory.Equals("Cena"))
            {
                return new CenaFactory();
            }
            if (factory.Equals("Cocktail"))
            {
                return new CocktailFactory();
            }

            return null;
        }
    }

    abstract class AbstractFactory
    {
        abstract public AbstractEvento getAbstractEvento(string evento);
    }

    abstract class AbstractEvento
    {
        //Atributos Obligatorios
        public int Id { get; set; }
        public int PersonalBase { get; set; }
        public float ValorBase { get; set; }
        public int Asistentes { get; set; }
        public int PersonalAdicional { get; set; }
        public string NombreEvento { get; set; }

        //Atributos virtuales
        public virtual bool isVegan { get; set; }
        public virtual bool isMusicaAmbiental { get; set; }
        public virtual string Ambientacion { get; set; }
        public virtual bool isLocal { get; set; }
        public virtual float OtroValorLocal { get; set; }


        //public virtual float RecargoAsistentes { get; set; }
        //public virtual float RecargoPersonalAdicional { get; set; }
        //public virtual float RecargoAmbientacion { get; set; }
        //public virtual float RecargoMusicaAmbiental { get; set; }
        //public virtual float RecargoLocal { get; set; }



        abstract public float getValorTotalEvento();
        abstract public float recargoAsistentes();
        abstract public float recargoPersonal();
        abstract public float recargoAmbientacion();
        abstract public float recargoMusicaAmbiental();
        abstract public float recargoLocal();
    }

    //Factorys
    class CoffeBreakFactory : AbstractFactory
    {
        public override AbstractEvento getAbstractEvento(string evento)
        {
            if (evento.Equals("LightBreak"))
            {
                var lightBreak = new CoffeBreak("LightBreak");
                lightBreak.ValorBase = 3f;
                lightBreak.PersonalBase = 2;
                //lightBreak.
                return lightBreak;
            }
            if (evento.Equals("JournalBreak"))
            {
                var journalBreak = new CoffeBreak("JournalBreak");
                journalBreak.ValorBase = 8f;
                journalBreak.PersonalBase = 6;
                return journalBreak;
            }
            if (evento.Equals("DayBreak"))
            {
                var dayBreak = new CoffeBreak("DayBreak");
                dayBreak.ValorBase = 12f;
                dayBreak.PersonalBase = 6;
                return dayBreak;
            }
            return null;
        }
    }

    //Clases concretas
    class CoffeBreak : AbstractEvento
    {
        public CoffeBreak()
        {
        }

        public CoffeBreak(string nombre)
        {
            this.NombreEvento = nombre;
        }

        public override float getValorTotalEvento()
        {
            return this.ValorBase + recargoAsistentes() + recargoPersonal();
        }

        public override float recargoAsistentes()
        {
            if (this.Asistentes >= 1 && this.Asistentes <= 20)
            {
                return 3f;
            }
            if (this.Asistentes >= 21 && this.Asistentes <= 50)
            {
                return 5f;
            }
            if (this.Asistentes > 50)
            {
                //Implementar este metodo
                return 0f;
            }
            return 0f;
        }

        public override float recargoPersonal()
        {
            if (this.PersonalAdicional == 2)
            {
                return 2f;
            }
            if (this.PersonalAdicional == 3)
            {
                return 3f;
            }
            if (this.PersonalAdicional == 4)
            {
                return 3.5f;
            }
            if (this.PersonalAdicional > 4)
            {
                var cantidad = this.PersonalAdicional - 4;
                var precio = (cantidad * 0.5f) + 3.5;
                return (float)precio;
            }
            return 0f;
        }
        public override float recargoAmbientacion()
        {
            return 0f;
        }
        public override float recargoLocal()
        {
            return 0f;
        }

        public override float recargoMusicaAmbiental()
        {
            return 0f;
        }
    }



    class CenaFactory : AbstractFactory
    {
        public override AbstractEvento getAbstractEvento(string evento)
        {
            if (evento.Equals("Ejecutiva"))
            {
                var ejecutiva = new Cena("Ejecutiva");
                ejecutiva.ValorBase = 25f;
                ejecutiva.Ambientacion = "Basica";
                ejecutiva.isMusicaAmbiental = false;
                ejecutiva.PersonalBase = 10;
                ejecutiva.isLocal = true;
                return ejecutiva;
            }
            if (evento.Equals("Celebracion"))
            {
                var celebracion = new Cena("Celebracion");
                celebracion.ValorBase = 35f;
                celebracion.PersonalBase = 14;
                celebracion.isMusicaAmbiental = false;
                celebracion.Ambientacion = "Basica";
                celebracion.isLocal = true;
                return celebracion;
            }
            return null;
        }
    }
    class Cena : AbstractEvento
    {
        public Cena()
        {
        }
        public Cena(string nombre)
        {
            this.NombreEvento = nombre;
        }

        public override float getValorTotalEvento()
        {
            //return this.ValorBase + recargoAsistentes() + recargoPersonal();
            return this.ValorBase + recargoAsistentes() + recargoPersonal() + recargoAmbientacion() + recargoMusicaAmbiental() + recargoLocal();
        }

        public override float recargoAmbientacion()
        {
            if (this.Ambientacion.Equals("Basica"))
            {
                return 3f;
            }
            if (this.Ambientacion.Equals("Personalizada"))
            {
                return 5f;
            }

            return 0;
        }

        public override float recargoAsistentes()
        {
            if (this.Asistentes >= 1 && this.Asistentes <= 20)
            {
                return this.Asistentes * 1.5f;
            }
            if (this.Asistentes >= 21 && this.Asistentes <= 50)
            {
                return this.Asistentes * 1.2f;
            }
            if (this.Asistentes > 50)
            {
                return this.Asistentes * 1f;
            }
            return 0f;
        }

        public override float recargoLocal()
        {
            if (this.isLocal)
            {
                return 9f;
            }
            else
            {
                return this.OtroValorLocal;
            }
        }

        public override float recargoMusicaAmbiental()
        {
            if (this.isMusicaAmbiental)
            {
                return 1.5f;
            }
            else
            {
                return 0f;
            }
        }

        public override float recargoPersonal()
        {
            if (this.PersonalAdicional == 2)
            {
                return 3f;
            }
            if (this.PersonalAdicional == 3)
            {
                return 4f;
            }
            if (this.PersonalAdicional == 4)
            {
                return 5f;
            }
            if (this.PersonalAdicional > 4)
            {
                var cantidad = this.PersonalAdicional - 4;
                var precio = (cantidad * 0.5f) + 5;
                return (float)precio;
            }
            return 0f;
        }
    }

    class CocktailFactory : AbstractFactory
    {
        public override AbstractEvento getAbstractEvento(string evento)
        {
            if (evento.Equals("QuickCocktail"))
            {
                var quickCocktail = new Cocktail("QuickCocktail");
                quickCocktail.ValorBase = 6f;
                quickCocktail.PersonalBase = 4;
                quickCocktail.Ambientacion = "No";
                quickCocktail.isMusicaAmbiental = false;
                return quickCocktail;
            }
            if (evento.Equals("AmbientCocktail"))
            {
                var ambientCocktail = new Cocktail("AmbientCocktail");
                ambientCocktail.ValorBase = 10f;
                ambientCocktail.PersonalBase = 5;
                ambientCocktail.Ambientacion = "No";
                ambientCocktail.isMusicaAmbiental = false;
                return ambientCocktail;
            }
            return null;
        }
    }

    class Cocktail : AbstractEvento
    {
        public Cocktail(string nombre)
        {
            this.NombreEvento = nombre;
        }
        public override float getValorTotalEvento()
        {
            //return this.ValorBase + recargoAsistentes() + recargoPersonal();
            return this.ValorBase + recargoAsistentes() + recargoPersonal() + recargoAmbientacion() + recargoMusicaAmbiental();
        }

        public override float recargoAmbientacion()
        {
            if (this.Ambientacion.Equals("Basica"))
            {
                return 2f;
            }
            if (this.Ambientacion.Equals("Personalizada"))
            {
                return 5f;
            }

            return 0;
        }

        public override float recargoAsistentes()
        {
            if (this.Asistentes >= 1 && this.Asistentes <= 20)
            {
                return 4f;
            }
            if (this.Asistentes >= 21 && this.Asistentes <= 50)
            {
                return 6f;
            }
            if (this.Asistentes > 50)
            {
                //Implementar este metodo
                return 0;
            }
            return 0f;
        }

        public override float recargoLocal()
        {
            return 0f;
        }

        public override float recargoMusicaAmbiental()
        {
            if (this.isMusicaAmbiental)
            {
                return 1f;
            }
            else
            {
                return 0f;
            }
        }

        public override float recargoPersonal()
        {
            if (this.PersonalAdicional == 2)
            {
                return 2f;
            }
            if (this.PersonalAdicional == 3)
            {
                return 3f;
            }
            if (this.PersonalAdicional == 4)
            {
                return 3.5f;
            }
            if (this.PersonalAdicional > 4)
            {
                var cantidad = this.PersonalAdicional - 4;
                var precio = (cantidad * 0.5f) + 3.5;
                return (float)precio;
            }
            return 0f;
        }
    }
}

