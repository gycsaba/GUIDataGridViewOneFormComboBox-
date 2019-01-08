using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConnectToMysqlDatabase;

namespace GuiDataGridViewOneFormHalfaj
{
    public partial class FormHalfaj : Form
    {
        #region Osztaly tulajdonsagok
        ///<value>Adatbázis kapcsolattartó objektum</value>
        private MySQLDatabaseInterface mdi;
        ///<value>Adatbázisból való beolvasás után ebben a táblában vannak a halfajadatok</value>
        private DataTable halfajokDT;
        /// <value>Jelzi, hogy az adatok lettek-e módosítva, kell-e őket menteni</value>
        private bool lettModositva = false;
        #endregion

        #region Konstruktorok és form betöltése, kilépés
        public FormHalfaj()
        {
            InitializeComponent();
            beallitVezerloketIndulaskor();
        }

        /// <summary>
        /// Program befejezése, kilépés a programból
        /// Nem mentett adatok esetén figyelmeztetés és nem lép ki ha azt választja a felhasználó
        /// </summary>
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            if (lettModositva)
            {
                if (MessageBox.Show(
                        "Nem mentett adatok vannak! Valóban ki akar lépni?",
                        "Kilépés",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    mdi.close();
                    this.Close();
                }
            }
            else
            {
                mdi.close();
                this.Close();
            }
        }
        #endregion      

        #region Adatbazis muveletek
        /// <summary>
        /// Az adatábzis kapcsolat segítségével feltölti a DataGridView-t az adattábla minden adatával
        /// </summary>
        private void feltoltVezerlotAdatbazisbolMindenAdattal()
        {
            Adatbazis a = new Adatbazis();
            mdi = a.kapcsolodas();
            mdi.open();
            halfajokDT = mdi.getToDataTable("SELECT * FROM halfaj");
            dataGridViewHalfaj.DataSource = halfajokDT;
        }
        #endregion

        #region A kulonbozo gombok esemenyei
        /// <summary>
        /// Az adatbázisból való betöltés akkor indul el, ha a felhasználó rákattint a buttonLoad gombra
        /// </summary>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            feltoltVezerlotAdatbazisbolMindenAdattal();
            beallitVezerloketNemSzerkeszthetoAdateleressel();
        }
        /// <summary>
        /// A felhasználó szerekeszteni szeretné a táblát, a DataGridView megfelelő tulajdonságait megváltoztatjuk
        /// </summary>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            beallitVezerloketDataGridViewSzerkesztoUzemmodba();
        }
        /// <summary>
        /// A felhasználó fel akarja függeszteni az adatok szerkesztését
        /// és a módosítás előtti állapotot szeretné látni
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            feltoltVezerlotAdatbazisbolMindenAdattal();
            beallitVezerloketNemSzerkeszthetoAdateleressel();
        }
        /// <summary>
        /// Menteni akkor kell, ha lett módosítva az adatbázis
        /// Módosítás történhetett törléskor, módosításkor vagy új adat felvitelekor
        /// Az összes módosítást mentésre kerül az mdi megfelelő függvényével
        /// Mentés után a vezérlőket be kell állítani
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!lettModositva)
            {
                MessageBox.Show(
                    "Nem lett módosítva egy adat sem. Nem kell menteni az adatábzist!",
                    "Módosítás",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                mdi.updateChangesInTable(halfajokDT);
                beallitVezerloketNemSzerkeszthetoAdateleressel();
            }
        }
        /// <summary>
        /// Meghatározzuk a törlendő sort, és törléskor illik rákérdezni a szándékra
        /// Törlés a DataGridView-ban történik, az adatbázisba a Mentés gombra kattintás után
        /// </summary>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //meghatározzuk a kijelölt sort
                int sor = dataGridViewHalfaj.SelectedRows[0].Index;
                if (MessageBox.Show(
                        "Valóban törölni akarja a sort?",
                        "Törlés",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation
                    ) == DialogResult.Yes)
                {
                    //töröljük a sort a DataGridView-ból
                    dataGridViewHalfaj.Rows.RemoveAt(sor);
                    //Lehetővé tesszük a mentést
                    buttonSave.Visible = true;
                    lettModositva = true;
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Jelölje ki a törlendő sort!",
                    "Törlés",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
        }
        /// <summary>
        /// Beállítjuk a vezérlőket
        /// Az utolsó sorba görgetünk és ott engedjük meg az új adatfelvitelt
        /// Bizonyos oszlopokat vagy sorokat elérhetetlenné  tehetünk
        /// </summary>
        private void buttonNew_Click(object sender, EventArgs e)
        {
            beallitVezerloketUjAdatfelvitelkor();
            // Meghatározzuk jelenleg hány sor van a DataGridView-ban
            int sor = dataGridViewHalfaj.Rows.Count - 1;
            // Kijelöljük azt a sort, amelyik az utolsó sor után van (itt történik majd az új adatok beírása a felhasználó által).
            dataGridViewHalfaj.Rows[sor].Cells[1].Selected = true;
            //Abba a sorba, amelyikbe az első adatot írja a felhasználó írhatunk egy segítő szöveget a felhasználónak.
            dataGridViewHalfaj.Rows[sor].Cells[1].Value = "Írja ide az új adatot";
            // A DataGridViewnak, csak azt a sorát engedjük írni, amelyikben az új adatokat viszi fel a felhasználó. A többit csak olvasni tudja (látja) a felhasználó.
            dataGridViewHalfaj.ReadOnly = false;
            for (int i = 0; i < sor; i = i + 1)
                dataGridViewHalfaj.Rows[i].ReadOnly = true;
            //Ezután a DataGridView végére görgetjük az ablakot.
            dataGridViewHalfaj.FirstDisplayedScrollingRowIndex = sor;
            //Azt az oszlopot, amit a felhasználó nem módosíthat, védjük
            dataGridViewHalfaj.Columns[0].ReadOnly = true;
        }
        #endregion

        #region Vezérlők állapotainak beállítása
        /// <summary>
        ///  Vezérlők beállítása induláskor
        /// </summary>
        private void beallitVezerloketIndulaskor()
        {
            buttonNew.Visible = false;
            buttonEdit.Visible = false;
            buttonDelete.Visible = false;
            buttonSave.Visible = false;
            buttonCancel.Visible = false;
        }
        /// <summary>
        ///  Vezérlők beállítása adatbeolvasás után nem szerkeszthető állapotba
        /// </summary>
        private void beallitVezerloketNemSzerkeszthetoAdateleressel()
        {
            //gombok beállítása
            buttonNew.Visible = true;
            buttonNew.Enabled = true;
            buttonEdit.Visible = true;
            buttonEdit.Enabled = true;
            buttonDelete.Visible = true;
            buttonDelete.Enabled = true;
            buttonSave.Visible = false;
            buttonCancel.Visible = false;

            //dataGridView beállítása
            dataGridViewHalfaj.ReadOnly = true;
            dataGridViewHalfaj.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewHalfaj.AllowUserToDeleteRows = false;
        }
        /// <summary>
        /// Vezérlők beállítása amikor a felhasználó szerekeszti a DataGridView-t
        /// </summary>
        private void beallitVezerloketDataGridViewSzerkesztoUzemmodba()
        {
            //gombok beállítása
            buttonNew.Visible = false;
            buttonEdit.Visible = false;
            buttonDelete.Visible = false;
            buttonSave.Visible = true;

            //dataGridView beállítása
            dataGridViewHalfaj.ReadOnly = false;
            dataGridViewHalfaj.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewHalfaj.AllowUserToDeleteRows = false;

            //új gombot kell felvenni, mert a szerkesztés akár megszakítható is
            buttonCancel.Visible = true;
            //módosítás beállítása
            lettModositva = false;
        }
        /// <summary>
        /// Vezérlők beállítása amikor a felhasználó új adatot visz fel a DataGridView-ban
        /// Vezérlőket beállítjuk, hogy lehessen megszakítani vagy menteni az új adatfelvitelt
        /// </summary>
        private void beallitVezerloketUjAdatfelvitelkor()
        {
            buttonNew.Visible = false;
            buttonSave.Enabled = true;
            buttonSave.Visible = true;
            buttonEdit.Enabled = false;
            buttonDelete.Enabled = false;
            buttonCancel.Visible = true;
            buttonCancel.Enabled = true;

            dataGridViewHalfaj.AllowUserToAddRows = true;
            dataGridViewHalfaj.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }
        #endregion

        #region DataGridView események továbbfejlesztése
        /// <summary>
        /// Ha bármilyen változás történik a DataGridView-ban feljegyezzük, hogy mentéskor el tudjuk dönteni, kell-e menteni vagy nem
        /// </summary>
        private void dataGridViewHalfaj_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            lettModositva = true;
        }
        /// <summary>
        /// A DataGridView új adatfelvitelekor lehetőség van alapértelmezett értékeke megadására
        /// Ezt a lehetőséget használjuk ki az új azonosító meghatározására
        /// </summary>
        private void dataGridViewHalfaj_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            //A checkbox mezőknek meg kell adni kezdőértéket
            e.Row.Cells[4].Value = false;
            e.Row.Cells[5].Value = false;

            //Készítünk egy új kapcsolatot az adatbázishoz
            Adatbazis ujida = new Adatbazis();
            MySQLDatabaseInterface mdiujid = ujida.kapcsolodas();
            mdiujid.open();

            //Készítünk egy lekérdezést az új halid meghatározására
            int max;
            bool siker = int.TryParse(mdiujid.executeScalarQuery("SELECT MAX(halid) FROM halfaj"), out max);
            if (!siker)
            {
                MessageBox.Show(
                "Nem lehet megállapítani a következő rekord kulcsát. Adatbázis lekérdezési hiba. Új adat felvitele nem lehetséges",
                    "Hiba...",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            mdiujid.close();
            e.Row.Cells[0].Value = max + 1;
        }
        #endregion
    }
}
