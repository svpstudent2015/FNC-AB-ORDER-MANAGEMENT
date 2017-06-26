﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Security.Cryptography;
using System.Xml;
using DAL;

namespace DAL.RepositoryFolder
{
    public class UtsattningarRepository : Repository<Utsattningar>
    {
        public Utsattningar HamtaEnUsattning(int id)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    return db.Utsattningar.FirstOrDefault(x => x.ID == id);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public void SparaRedigeraUtsattningar(Utsattningar nyUts)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {

                    Utsattningar gammalUts = db.Utsattningar.FirstOrDefault(x => x.ID == nyUts.ID);

                    gammalUts.Ordernr = nyUts.Ordernr;
                    gammalUts.KundID = nyUts.KundID;
                    gammalUts.Telefonnr = nyUts.Telefonnr;
                    gammalUts.Ort = nyUts.Ort;
                    gammalUts.Adress = nyUts.Adress;
                    gammalUts.InDatum = nyUts.InDatum;
                    gammalUts.UtDatum = nyUts.UtDatum;
                    gammalUts.StyckPris = nyUts.StyckPris;
                    gammalUts.Langd = nyUts.Langd;
                    gammalUts.Timmar = nyUts.Timmar;
                    gammalUts.Fakturerad = nyUts.Fakturerad;
                    gammalUts.Ovrigt = nyUts.Ovrigt;
                    gammalUts.Status = nyUts.Status;
                    gammalUts.ID = nyUts.ID;
                    gammalUts.GPS = nyUts.GPS;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
        }

        public void TaBortEnUtsattning(int id)
        {
            try
            {
                using (var db = new FNCOrderHanteringEntitiesConnections())
                {
                    Utsattningar utsattningAttTaBort = db.Utsattningar.Where(x => x.ID==id)
                        .FirstOrDefault();
                    db.Entry(utsattningAttTaBort).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }

    }
}
