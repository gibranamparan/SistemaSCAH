using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for Utilidades
/// </summary>
public class Utilidades
{
	public Utilidades()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // ---- GetCellByName ----------------------------------
    //
    // pass in a GridViewRow and a database column name 
    // returns a DataControlFieldCell or null

    static public DataControlFieldCell GetCellByName(GridViewRow Row, String CellName)
    {
        foreach (DataControlFieldCell Cell in Row.Cells)
        {
            if (Cell.ContainingField.ToString() == CellName)
                return Cell;
        }
        return null;
    }

    // ---- GetColumnIndexByHeaderText ----------------------------------
    //
    // pass in a GridView and a Column's Header Text
    // returns index of the column if found 
    // returns -1 if not found 

    static public int GetColumnIndexByHeaderText(GridView aGridView, String ColumnText)
    {
        TableCell Cell;
        for (int Index = 0; Index < aGridView.HeaderRow.Cells.Count; Index++)
        {
            Cell = aGridView.HeaderRow.Cells[Index];
            if (Cell.Text.ToString() == ColumnText)
                return Index;
        }
        return -1;
    }

    // ---- GetColumnIndexByDBName ----------------------------------
    //
    // pass in a GridView and a database field name
    // returns index of the bound column if found 
    // returns -1 if not found 

    static public int GetColumnIndexByDBName(GridView aGridView, String ColumnText)
    {
        System.Web.UI.WebControls.BoundField DataColumn;

        for (int Index = 0; Index < aGridView.Columns.Count; Index++)
        {
            DataColumn = aGridView.Columns[Index] as System.Web.UI.WebControls.BoundField;

            if (DataColumn != null)
            {
                if (DataColumn.DataField == ColumnText)
                    return Index;
            }
        }
        return -1;
    }

    public static string numerosAElementosDeTabla(string p, DataTable dt)
    {
        p = p.Trim(',');
        String[] nums = p.Split(',');
        String resultado = String.Empty;
        DataTable temp;
        int intSt;
        int masUno = 0;
        if (!isZeroBased(dt))
        {
            masUno = 1;
        }
        if (nums.Length > 0)
            foreach (string st in nums)
            {
                if(int.TryParse(st,out intSt))
                {
                    var result = from registro in dt.AsEnumerable() where (int)registro.ItemArray[0] == (intSt+masUno) select registro;
                    temp = result.CopyToDataTable();
                    resultado += temp.Rows[0].ItemArray[1] + ", ";
                }
            }

        return resultado.Trim(new char[] { ',', ' ' });
    }

    //Valida y transforma String de TextBox a fecha
    public static bool validarFechaDeTextBox(ref TextBox tb, out DateTime valorEntero)
    {
        bool valido = true;
        tb.CssClass = String.Empty;
        valido = DateTime.TryParse(tb.Text.Trim(), out valorEntero);

        if (!valido)
            tb.CssClass = "validacionError";

        return valido;
    }

    //Tabla es zeroBased
    public static bool isZeroBased(DataTable dt)
    {
        DataRow dr = dt.Rows[0];
        int firstId = (int)dr.ItemArray[0];

        return firstId == 0;
    }
}