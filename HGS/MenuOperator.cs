using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HGS
{
    class MenuOperator : ContextMenuStrip
    {
        public MenuOperator()
        {
            AddItems("算术", ARITHMETIC);
            AddItems("比较", COMPARE);
        }
        void AddItems(string category, string functions)
        {
            var item = new ToolStripMenuItem(category);
            string[] ssst = functions.Split('\r', '\n');
            foreach (var fn in functions.Split('\r', '\n'))
            {
                var data = fn.Split('\t');
                if (data.Length == 3)
                {
                    var x = item.DropDownItems.Add(data[0].Trim());
                    x.ToolTipText = string.Format("{0}\r\n{1}", data[1].Trim(), data[2].Trim());
                }
            }
            item.DropDownItemClicked += (s, e) =>
                {
                    this.OnItemClicked(new ToolStripItemClickedEventArgs(e.ClickedItem));
                };
            Items.Add(item);
        }

        // function names, descriptions, and parameters (from excel sheet in Documentation folder)
        const string ARITHMETIC =
            @"+	加	+
            －	减	-
            *	乘	*
            /	除	/
            \\	整除	\\
            ^	乘方	^
            (	左括号	(
            )	右括号	)";
        const string COMPARE =
            @"=	等于	=
            <>	不等于	<>
            >	大于	>
            >=	大于等	>=
            <	小于	<
            <=	小于等	<=";
    }
}
