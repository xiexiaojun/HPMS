﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Tool;

namespace HPMS.Code.Utility
{
    public static class Extende
    {


        public static T GetNumVali<T>(this NumericUpDown numBox,int max,int min, Func<NumericUpDown,int,int, bool> check)
            where T : struct
        {
            if (check(numBox,max,min))
            {
                var type = typeof(T);
                var method = type.GetMethod("TryParse", new Type[] { typeof(string), type.MakeByRefType() });
                var parameters = new object[] { numBox.Value.ToString(), default(T) };

                // 若转换失败，执行failed
                method.Invoke(null, parameters);
                return (T)parameters[1];


            }
            else
            {
                SelectTab(numBox);
                numBox.Focus();
                numBox.Select();
                Ui.MessageBoxMuti(string.Format("超出范围,输入值必须在{0}和{1}之间", max, min));
                throw new InvalidCastException("输入值格式不正确");
            }
        }

        public static T GetNumVali<T>(this NumericUpDown numBox, Func<NumericUpDown, bool> check, Action<NumericUpDown> failed)
        where T:struct 
        {
            if (check(numBox))
            {
                return (T)(object) numBox.Value;
            }
            else
            {
                failed(numBox);
                throw new InvalidCastException("输入值格式不正确");
            }
        }

        public static T GetNumVali<T>(this NumericUpDown numBox, Func<NumericUpDown, bool> check)
            where T : struct
        {
            return GetNumVali<T>(numBox, check, p =>
            {
                p.Focus();
               // p.SelectAll();
                Ui.MessageBoxMuti("输入值超出范围");
            });

        }

        public static T GetNumVali<T>(this NumericUpDown numBox)
            where T : struct
        {
            return GetNumVali<T>(numBox, p => p.Value==0);

        }


        public static string GetTextVali(this TextBoxX textBox, Func<TextBoxX, bool> check, Action<TextBoxX> failed)
        {
            if (check(textBox))
            {
                return textBox.Text.Trim();
            }
            else
            {
                failed(textBox);
                throw new InvalidCastException("输入值格式不正确");
            }
        }

        public static string GetTextVali(this TextBoxX textBox, Func<TextBoxX, bool> check)
        {
            return GetTextVali(textBox, check, p =>
            {
                SelectTab(p);
                p.Select();
                p.Focus();
                p.FocusHighlightColor=Color.Blue;
                p.SelectAll();
                Ui.MessageBoxMuti("输入值不能为空");
            });

        }

        public static void SelectTab(Control ctl)
        {
            var temp = ctl;
            var form = FindForm(ctl);
            if(form.WindowState==FormWindowState.Minimized)
                return;
            while (temp.Parent != null)
            {
                temp = temp.Parent;
                if (temp is TabPage)
                {
                    var tabControls = (System.Windows.Forms.TabControl)temp.Parent;
                    tabControls.SelectTab((TabPage)temp);
                
                }
                else if (temp is TabControlPanel)
                {
                    var tabControls = (DevComponents.DotNetBar.TabControl)temp.Parent;
                    tabControls.SelectedPanel=((TabControlPanel)temp);

                }
            }
        }

        public static void SelectTab(DevComponents.DotNetBar.TabControl tabControl, string tabText)
        {
           
            var tabsCollection = tabControl.Tabs;
            for (int i = 0; i < tabsCollection.Count; i++)
            {
                if (tabsCollection[i].Text == tabText)
                {
                    tabControl.SelectedTabIndex = i;
                    return;
                }
            }
        }

        public static Form FindForm(Control c)
        {
            if (c is Form)
                return c as Form;
            if (c.Parent != null)
                return FindForm(c.Parent);
            return null;
        }

        public static string GetTextVali(this TextBoxX textBox)
        {
            return GetTextVali(textBox, p => p.Text.Trim().Length != 0);

        }


        public static T GetValue<T>(this TextBoxX textBox, Action<TextBoxX> failed)
             where T : struct
        {
            var type = typeof(T);
            var method = type.GetMethod("TryParse", new Type[] { typeof(string), type.MakeByRefType() });
            var parameters = new object[] { textBox.Text, default(T) };

            // 若转换失败，执行failed
            if (!(bool)method.Invoke(null, parameters))
            {
                failed(textBox);
                throw new InvalidCastException("输入值格式不正确，请检查输入值。");
            }

            return (T)parameters[1];
        }



        public static T GetValue<T>(this TextBoxX textBox, bool isShowError)
            where T : struct
        {
            return GetValue<T>(textBox, p =>
            {
                if (isShowError)
                {
                    p.Focus();
                    p.SelectAll();
                    MessageBox.Show("输入值格式不正确，请重新输入！",
                        "提示--值类型：" + typeof(T).Name,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            });
        }

        public static T GetValue<T>(this TextBoxX textBox)
            where T : struct
        {
            return GetValue<T>(textBox, true);
        }


        public static string GetValue(this TextBoxX textBox, Action<TextBoxX> failed)
        {
            if (textBox.Text.Trim() == "")
            {
                failed(textBox);
                throw new InvalidExpressionException("输入值不能为空");
            }
            return textBox.Text.Trim();
        }

        public static string GetValue(this TextBoxX textBox, bool isShowError)
        {
            return GetValue(textBox, p =>
            {
                if (isShowError)
                {
                    p.Focus();
                    p.SelectAll();
                    MessageBox.Show("输入值异常", "输入值不能为空", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            });
        }

        public static string GetValue(this TextBoxX textBox)
        {
            return GetValue(textBox, true);
        }

        public static string GetValue(this ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null)
            {
                SelectTab(comboBox);
                comboBox.Focus();
                comboBox.SelectAll();
                throw new InvalidExpressionException("输入值不能为空");
            }
            else
            {
                return comboBox.SelectedItem.ToString();
            }
        }

        public static void Addsafe(this Dictionary<string, string> dic, string key, string value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key,value);
            }
        }

        public static void ClearChecked(this CheckedListBox chkListBox)
        {
            if (chkListBox.InvokeRequired)
            {

                Action<CheckedListBox> dSetListbox = ClearChecked;
                chkListBox.BeginInvoke(dSetListbox, new object[] { chkListBox });
            }
            else
            {
                foreach (int variable in chkListBox.CheckedIndices)
                {
                    chkListBox.SetItemChecked(variable, false);
                }
            }


           
        }
    }
}
