using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PrintGoodsInBill
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 定义全局变量及对象
        string strCon = "server=localhost;database=tsJXC;uid=sa;pwd=1";
        public static string strID = "";
        public static string strInPeople = "";
        public static string strInProvider = "";
        public static string strPlace = "";
        public static string strGID = "";
        public static string strGName = "";
        public static string strGSpec = "";
        public static string strGUnit = "";
        public static string strGMoney = "";
        public static string strGNum = "";
        public static string strSMoney = "";
        public static string strInDate = "";
        public static string strRemark = "";
        SqlConnection sqlcon;
        SqlCommand sqlcmd;
        SqlDataAdapter sqlda;
        DataSet myds;
        #endregion

        //窗体初始化时显示所有入库信息
        private void Form1_Load(object sender, EventArgs e)
        {
            dgvInfo.DataSource = SelectIGInfo("", "").Tables[0];
        }

        //根据选中的入库单显示其详细信息
        private void dgvInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    myds = SelectIGInfo("编号", dgvInfo.Rows[e.RowIndex].Cells[0].Value.ToString());
            //    strID = myds.Tables[0].Rows[0][0].ToString();
            //    strInPeople = myds.Tables[0].Rows[0][1].ToString();
            //    strInProvider = myds.Tables[0].Rows[0][2].ToString();
            //    strPlace = myds.Tables[0].Rows[0][3].ToString();
            //    strGID = myds.Tables[0].Rows[0][4].ToString();
            //    strGName = myds.Tables[0].Rows[0][5].ToString();
            //    strGSpec = myds.Tables[0].Rows[0][6].ToString();
            //    strGUnit = myds.Tables[0].Rows[0][7].ToString();
            //    strGMoney = "￥" + myds.Tables[0].Rows[0][8].ToString();
            //    strGNum = myds.Tables[0].Rows[0][9].ToString();
            //    strSMoney = "￥" + myds.Tables[0].Rows[0][10].ToString();
            //    strInDate = myds.Tables[0].Rows[0][11].ToString();
            //    strRemark = myds.Tables[0].Rows[0][12].ToString();
            //}
            //catch { }
        }

        //打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
            //printPreviewDialog1.
            //Click += PrintPreviewControl;
            //printDocument1_PrintPage();
            //btnPrint.Click += new EventHandler(printDocument1_PrintPage);
            printDocument1.Print();
        }

        //设置打印的商品入库单据
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int printWidth = e.PageBounds.Width;
            int printHeight = e.PageBounds.Height;
            int left = printWidth / 2 - 305;
            int right = printWidth / 2 + 305;
            int top = printHeight / 2-200;
            Brush myBrush = new SolidBrush(Color.Black);
            Pen mypen = new Pen(Color.Black);
            Font myFont = new Font("宋体", 12);
            e.Graphics.DrawString("商品入库单", new Font("宋体", 20, FontStyle.Bold), myBrush, new Point(printWidth / 2 - 100, top));
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 300, top + 30, 480, top + 30);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 300, top + 34, 480, top + 34);
            e.Graphics.DrawString("吉林省明日科技有限公司", new Font("宋体", 9), myBrush, new Point(left + 2, top + 25));
            e.Graphics.DrawString("日期：" + DateTime.Now.ToLongDateString(), new Font("宋体", 12), myBrush, new Point(right - 190, top + 25));
            e.Graphics.DrawRectangle(mypen, left, top + 42, 610, 230);//绘制矩形框
            e.Graphics.DrawLine(mypen, left, top + 72, left + 610, top + 72);//第一行
            e.Graphics.DrawLine(mypen, left, top + 102, left + 610, top + 102);//第二行
            e.Graphics.DrawLine(mypen, left, top + 132, left + 610, top + 132);//第三行
            e.Graphics.DrawLine(mypen, left, top + 162, left + 610, top + 162);//第四行
            e.Graphics.DrawLine(mypen, left + 80, top + 42, left + 80, top + 272);//第一列
            e.Graphics.DrawLine(mypen, left + 220, top + 42, left + 220, top + 72);//第二列
            e.Graphics.DrawLine(mypen, left + 280, top + 42, left + 280, top + 72);//第三列
            e.Graphics.DrawLine(mypen, left + 410, top + 42, left + 410, top + 132);//第四列
            e.Graphics.DrawLine(mypen, left + 470, top + 42, left + 470, top + 162);//第五列
            e.Graphics.DrawLine(mypen, left + 170, top + 102, left + 170, top + 162);//第三行第二列
            e.Graphics.DrawLine(mypen, left + 220, top + 102, left + 220, top + 162);//第三行第三列
            e.Graphics.DrawLine(mypen, left + 300, top + 132, left + 300, top + 162);//第四行第四列
            e.Graphics.DrawLine(mypen, left + 360, top + 132, left + 360, top + 162);//第四行第五列
            e.Graphics.DrawLine(mypen, left + 520, top + 132, left + 520, top + 162);//第四行第七列
            //第一行数据
            e.Graphics.DrawString("入库日期", myFont, myBrush, new Point(left + 2, top + 50));
            e.Graphics.DrawString(strInDate, myFont, myBrush, new Point(left + 82, top + 50));
            e.Graphics.DrawString("单据号", myFont, myBrush, new Point(left + 222, top + 50));
            e.Graphics.DrawString(strID, myFont, myBrush, new Point(left + 282, top + 50));
            e.Graphics.DrawString("入库人", myFont, myBrush, new Point(left + 412, top + 50));
            e.Graphics.DrawString(strInPeople, myFont, myBrush, new Point(left + 472, top + 50));
            //第二行数据
            e.Graphics.DrawString("供货商", myFont, myBrush, new Point(left + 2, top + 80));
            e.Graphics.DrawString(strInProvider, myFont, myBrush, new Point(left + 82, top + 80));
            e.Graphics.DrawString("产地", myFont, myBrush, new Point(left + 412, top + 80));
            e.Graphics.DrawString(strPlace, myFont, myBrush, new Point(left + 472, top + 80));
            //第三行数据
            e.Graphics.DrawString("商品编号", myFont, myBrush, new Point(left + 2, top + 110));
            e.Graphics.DrawString(strGID, myFont, myBrush, new Point(left + 82, top + 110));
            e.Graphics.DrawString("名称", myFont, myBrush, new Point(left + 172, top + 110));
            e.Graphics.DrawString(strGName, myFont, myBrush, new Point(left + 222, top + 110));
            e.Graphics.DrawString("规格", myFont, myBrush, new Point(left + 412, top + 110));
            e.Graphics.DrawString(strGSpec, myFont, myBrush, new Point(left + 472, top + 110));
            //第四行数据
            e.Graphics.DrawString("单位", myFont, myBrush, new Point(left + 2, top + 140));
            e.Graphics.DrawString(strGUnit, myFont, myBrush, new Point(left + 82, top + 140));
            e.Graphics.DrawString("单价", myFont, myBrush, new Point(left + 172, top + 140));
            e.Graphics.DrawString(strGMoney, myFont, myBrush, new Point(left + 222, top + 140));
            e.Graphics.DrawString("数量", myFont, myBrush, new Point(left + 302, top + 140));
            e.Graphics.DrawString(strGNum, myFont, myBrush, new Point(left + 362, top + 140));
            e.Graphics.DrawString("金额", myFont, myBrush, new Point(left + 472, top + 140));
            e.Graphics.DrawString(strSMoney, myFont, myBrush, new Point(left + 522, top + 140));
            //第五行数据
            e.Graphics.DrawString("备注", myFont, myBrush, new Point(left + 2, top + 170));
            e.Graphics.DrawString(strRemark, myFont, myBrush, new Point(left + 82, top + 170));
        }

        #region 获得数据库连接
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <returns>返回SqlConnection对象</returns>
        private SqlConnection getCon()
        {
            sqlcon = new SqlConnection(strCon);
            sqlcon.Open();
            return sqlcon;
        }
        #endregion

        #region 查询商品入库信息
        /// <summary>
        /// 查询商品入库信息
        /// </summary>
        /// <param name="str">查询条件</param>
        /// <param name="str">查询关键字</param>
        /// <returns>DataSet数据集对象</returns>
        private DataSet SelectIGInfo(string str, string strKeyWord)
        {
            sqlcon = getCon();
            sqlda = new SqlDataAdapter();
            sqlcmd = new SqlCommand("select * from t_Item", sqlcon);
           // sqlcmd.CommandType = CommandType.StoredProcedure;
            //switch (str)
            //{
            //    case "编号":
            //        sqlcmd.Parameters.Add("@id", SqlDbType.VarChar, 20).Value = strKeyWord;
            //        break;
            //    default:
            //        sqlcmd.Parameters.Add("@id", SqlDbType.VarChar, 20).Value = "";
            //        break;
            //}
            sqlda.SelectCommand = sqlcmd;
            myds = new DataSet();
            sqlda.Fill(myds);
            sqlcon.Close();
            return myds;
        }
        #endregion
    }
}
