﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class ViewEditorial : EditorialModel
    {
        DataTable ediTbl = new DataTable();

        public ViewEditorial(int editorialID)
        {
            string sql = "select * from Editorial, [User], Tag, Problem, OnlineJudge where Editorial.EditorialID=6002 and Editorial.UserID=[User].UserID and Editorial.TagID=Tag.TagID and Editorial.ProblemID=Problem.ProblemID and Problem.OJID=OnlineJudge.OJID";
            ediTbl = new DBHelper().getTable(sql);

            ProblemTitle = Convert.ToString(ediTbl.Rows[0][18]);
            Description = Convert.ToString(ediTbl.Rows[0][4]);
            TagName = Convert.ToString(ediTbl.Rows[0][15]);
            UserName = Convert.ToString(ediTbl.Rows[0][9]);
            TagID = Convert.ToInt32(ediTbl.Rows[0][3]);
        }

        
    }
}