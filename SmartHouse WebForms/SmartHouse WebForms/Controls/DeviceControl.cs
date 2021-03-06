﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Smart_House;

namespace SmartHouse_WebForms
{
    public class DeviceControl : Panel
    {
        private int id; // Ключ девайса в коллекции девайсов

        private readonly IDictionary<int, Device> DevicesDictionary;

        private IChangeEnum chen;

        private IOpenOrClose orc;
        private IResetSettings rs;
        private ISpeed sp;

        private IUse us;
        private IVolume vol;

        private Label status; // Поле вывода текущего состояние устройства
        private Button onButton; // Кнопка включить устройство
        private Button offButton; // Кнопка выключить устройство
        public DeviceControl(int id, IDictionary<int, Device> devicesDictionary)
        {
            this.id = id;
            DevicesDictionary = devicesDictionary;
            Initializer();
        }
        protected void Initializer()
        {
            orc = (IOpenOrClose)DevicesDictionary[id];
            CssClass = "device-div";

            Label name = new Label();
            name.ID = DevicesDictionary[id].Name + id.ToString();
            name.Text = DevicesDictionary[id].Name;
            name.CssClass = "lblName";
            Controls.Add(name);
            //------
            status = new Label();
            status.ID = status + id.ToString();
            status.Text = DevicesDictionary[id] + "<br>";
            status.CssClass = "lblSt";
            Controls.Add(status);
            //======================================
            if (DevicesDictionary[id].Status == false)
            {
                onButton = Button("on ", "switchOff", "Switch On"); // id, CssClass, ToolTip
                onButton.Click += ButtonClick;
                Controls.Add(onButton);
                Controls.Add(Span("<br />"));
            }
            //=========
            if (DevicesDictionary[id].Status == true)
            {
                offButton = Button("off ", "switchOn", "Switch Off");
                offButton.Click += ButtonClick;
                Controls.Add(offButton);
                Controls.Add(Span("<br />"));
            }
            //======================================
            if (DevicesDictionary[id] is IChangeEnum)
            {
                Button PreviousEnum = Button("prEn ", "leftBt", "Previous");
                PreviousEnum.Click += ButtonClick;
                Controls.Add(PreviousEnum);
                //=========
                if (DevicesDictionary[id] is IFridgeMark)
                {
                    Label lb = Label("templvl ", "lbl", "Temperature Lvl");
                    Controls.Add(lb);
                }
                else if (DevicesDictionary[id] is IHooverMark)
                {
                    Label lb = Label("cllvl ", "lbl", "Clean Lvl");
                    Controls.Add(lb);
                }
                else if (DevicesDictionary[id] is IBicycleMark)
                {
                    Label lb = Label("rellvl ", "lbl", "Relief Lvl");
                    Controls.Add(lb);
                }
                else if (DevicesDictionary[id] is ITelevisionMark)
                {
                    Label lb = Label("chlvl ", "lbl", "Channel");
                    Controls.Add(lb);
                }
                else if (DevicesDictionary[id] is IWarhammerMark)
                {
                    Label lb = Label("whlvl ", "lbl", "Battle Place");
                    Controls.Add(lb);
                }
                //=========
                Button NextEnum = Button("nxCh ", "rightBt", "Next");
                NextEnum.Click += ButtonClick;
                Controls.Add(NextEnum);
                Controls.Add(Span("<br />"));
            }
            if (DevicesDictionary[id] is IVolume)
            {
                Button PreviousVol = Button("prVol ", "leftBt", "Minus Volume");
                PreviousVol.Click += ButtonClick;
                Controls.Add(PreviousVol);
                //=========
                Label lbCh = Label("vol ", "lbl", "Volume");
                Controls.Add(lbCh);
                //=========
                Button NextVol = Button("nxVol ", "rightBt", "Plus Volume");
                NextVol.Click += ButtonClick;
                Controls.Add(NextVol);
                Controls.Add(Span("<br />"));
            }
            //======================================
            if (DevicesDictionary[id] is IOpenOrClose)
            {
                Label lbCh = Label("opOrCl ", "lblCenter", "Open or Close");
                Controls.Add(lbCh);
                Controls.Add(Span("<br />"));
                //=========
                if (orc.StatusOpen == false)
                {
                    Button op = Button("open ", "switchOff", "Open"); // id, CssClass, ToolTip
                    op.Click += ButtonClick;
                    Controls.Add(op);
                }
                //=========
                if (orc.StatusOpen == true)
                {
                    Button cl = Button("close ", "switchOn", "Close");
                    cl.Click += ButtonClick;
                    Controls.Add(cl);
                }
                Controls.Add(Span("<br />"));
            }
            //======================================
            if (DevicesDictionary[id] is IResetSettings)
            {
                if (DevicesDictionary[id] is IBicycleMark)
                {
                    Label fl = Label("firstParBy ", "lbl", "Reset Pulse");
                    Controls.Add(fl);
                    //=========
                    Button rfp = Button("fps ", "reset", "Reset Pulse");
                    rfp.Click += ButtonClick;
                    Controls.Add(rfp);
                    Controls.Add(Span("<br />"));
                    //======================================
                    Label sl = Label("secondPar ", "lbl", "Reset Speed");
                    Controls.Add(sl);
                    //=========
                    Button rsp = Button("sps ", "reset", "Reset Speed");
                    rsp.Click += ButtonClick;
                    Controls.Add(rsp);
                    Controls.Add(Span("<br />"));
                }
                else if (DevicesDictionary[id] is IHooverMark)
                {
                    Label fl = Label("firstPar ", "lbl", "Reset Dust Collector");
                    Controls.Add(fl);
                    //=========
                    Button rfp = Button("fps ", "reset", "Reset Dust Collector");
                    rfp.Click += ButtonClick;
                    Controls.Add(rfp);
                    Controls.Add(Span("<br />"));
                    //======================================
                    Label sl = Label("secondPar ", "lbl", "Сharge Accumulator");
                    Controls.Add(sl);
                    //=========
                    Button rsp = Button("sps ", "reset", "harge Accumulator");
                    rsp.Click += ButtonClick;
                    Controls.Add(rsp);
                    Controls.Add(Span("<br />"));
                }
            }
            //======================================
            if (DevicesDictionary[id] is ISpeed)
            {
                Button low = Button("low ", "btbLow", "Speed = low");
                low.Click += ButtonClick;
                Controls.Add(low);
                //=========
                Button unh = Button("unh ", "btbUnh", "ISpeed = Unhurriedly");
                unh.Click += ButtonClick;
                Controls.Add(unh);
                Controls.Add(Span("<br />"));
                //======================================
                Button boost = Button("boost ", "btbBoost", "ISpeed = Boost");
                boost.Click += ButtonClick;
                Controls.Add(boost);
                //=========
                Button quick = Button("quick ", "btbQuick", "ISpeed = Quick");
                quick.Click += ButtonClick;
                Controls.Add(quick);
                Controls.Add(Span("<br />"));
            }
            //======================================
            if (DevicesDictionary[id] is IUse)
            {
                Button app = Button("apply ", "btbUseDev", "Use");
                app.Click += ButtonClick;
                Controls.Add(app);
            }
            Button deleteButton = Button("remove ", "btbDelete", "Delete Device");
            deleteButton.Click += DeleteButtonClick;
            Controls.Add(deleteButton);
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            DevicesDictionary.Remove(id); // Удаление девайса из коллекции
            Parent.Controls.Remove(this); // Удаление графики для девайса
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (DevicesDictionary[id] is IChangeEnum)
            {
                chen = (IChangeEnum)DevicesDictionary[id];
            }
            if (DevicesDictionary[id] is IVolume)
            {
                vol = (IVolume)DevicesDictionary[id];
            }
            if (DevicesDictionary[id] is IOpenOrClose)
            {
                orc = (IOpenOrClose)DevicesDictionary[id];
            }
            if (DevicesDictionary[id] is IResetSettings)
            {
                rs = (IResetSettings)DevicesDictionary[id];
            }
            if (DevicesDictionary[id] is ISpeed)
            {
                sp = (ISpeed)DevicesDictionary[id];
            }
            if (DevicesDictionary[id] is IUse)
            {
                us = (IUse)DevicesDictionary[id];
            }
            string[] mass = button.ID.Split(new char[] { ' ' });
            switch (mass[0])
            {
                case "on":
                    DevicesDictionary[id].On();
                    break;
                case "off":
                    DevicesDictionary[id].Off();
                    break;
                case "prEn":
                    chen.PreviousEnum();
                    break;
                case "nxCh":
                    chen.NextEnum();
                    break;
                case "prVol":
                    vol.MinusVolume();
                    break;
                case "nxVol":
                    vol.PlusVolume();
                    break;
                case "close":
                    orc.Close();
                    break;
                case "open":
                    orc.Open();
                    break;
                case "fps":
                    rs.ResetFirstParameter();
                    break;
                case "sps":
                    rs.ResetSecondParameter();
                    break;
                case "low":
                    sp.Low();
                    break;
                case "unh":
                    sp.Unhurriedly();
                    break;
                case "boost":
                    sp.Boost();
                    break;
                case "quick":
                    sp.Quick();
                    break;
                case "apply":
                    us.Apply();
                    break;
            }
            Controls.Clear();
            Initializer();
        }

        protected HtmlGenericControl Span(string innerHTML)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHTML;
            return span;
        }

        protected Button Button(string pref, string csscl, string tt)
        {
            Button button = new Button();
            button.ID = pref + id;
            button.CssClass = csscl;
            button.ToolTip = tt;
            return button;
        }
        protected Label Label(string pref, string csscl, string txt)
        {
            Label label = new Label();
            label.ID = pref + id;
            label.CssClass = csscl;
            label.Text = txt;
            return label;

        }
    }
}