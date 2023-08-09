﻿using System;
using System.Linq;
using Alternet.UI;
using Alternet.Drawing;
using System.Collections.Generic;

namespace ControlsSample
{
    internal partial class TextInputPage : Control
    {
        private const string LoremIpsum = "Lorem ipsum dolor sit amet,\nconsectetur adipiscing elit. Suspendisse tincidunt orci vitae arcu congue commodo. Proin fermentum rhoncus dictum.";
        
        private IPageSite? site;

        public TextInputPage()
        {
            InitializeComponent();
            multiLineTextBox.EmptyTextHint = "Sample Hint";
            textBox1.EmptyTextHint = "Sample Hint";
            multiLineTextBox.Text = LoremIpsum;
            multiLineTextBox.TextUrl += MultiLineTextBox_TextUrl;
        }

        private void ReadOnlyCheckBox_Changed(object? sender, EventArgs e) 
        {
            textBox1.ReadOnly = readOnlyCheckBox.IsChecked;
        }
        
        private void PasswordCheckBox_Changed(object? sender, EventArgs e)
        {
            textBox1.IsPassword = passwordCheckBox.IsChecked;
        }

        private void MultiLineTextBox_TextUrl(object? sender, EventArgs e)
        {
            string? url = multiLineTextBox.DoCommand("GetReportedUrl")?.ToString();
            site?.LogEvent("TextBox: Url clicked =>" + url);
        }

        public IPageSite? Site
        {
            get => site;

            set
            {
                site = value;
            }
        }

        private void TextBox_ValueChanged(object? sender, TextChangedEventArgs e)
        {
            site?.LogEvent("New TextBox value is: " + ((TextBox)sender!).Text);
        }

        private void HasBorderButton_Click(object? sender, EventArgs e)
        {
            foreach (var textBox in textBoxesPanel.Children.OfType<TextBox>())
                textBox.HasBorder = !textBox.HasBorder;
        }

        private void AddLetterAButton_Click(object? sender, EventArgs e)
        {
            foreach (var textBox in textBoxesPanel.Children.OfType<TextBox>())
                textBox.Text += "A";
        }

        private void GetWordIndex(
            string s, 
            string word, 
            out int startIndex,
            out int endIndex)
        {
            startIndex = s.IndexOf(word);
            if (startIndex < 0)
            {
                endIndex = -1;
                return;
            }
            endIndex = startIndex + word.Length;
        }

        private void RichEditButton_Click(object? sender, EventArgs e)
        {
            ITextBoxTextAttr taTextColorRed = TextBox.CreateTextAttr();
            taTextColorRed.SetTextColor(Color.Red);

            ITextBoxTextAttr taBackColorYellow = TextBox.CreateTextAttr();
            taBackColorYellow.SetBackgroundColor(Color.Yellow);

            ITextBoxTextAttr taUnderlined = TextBox.CreateTextAttr();
            taUnderlined.SetFontUnderlined();

            ITextBoxTextAttr taItalic = TextBox.CreateTextAttr();
            taItalic.SetFontItalic();

            ITextBoxTextAttr taBold = TextBox.CreateTextAttr();
            taBold.SetFontWeight(FontWeight.Bold);

            var homePage = @"https://www.alternet-ui.com/";

            ITextBoxTextAttr taUrl = TextBox.CreateTextAttr();
            taUrl.SetURL(homePage);

            ITextBoxTextAttr taDefault = TextBox.CreateTextAttr();

            ITextBoxTextAttr taUnorderedList = TextBox.CreateTextAttr();
            taUnorderedList.SetBulletStyle(TextBoxTextAttrBulletStyle.Standard);
            taUnorderedList.SetBulletName("standard/circle");

            ITextBoxTextAttr taOrderedList = TextBox.CreateTextAttr();
            taUnorderedList.SetBulletStyle(TextBoxTextAttrBulletStyle.Arabic);

            ITextBoxTextAttr taBig = TextBox.CreateTextAttr();
            taBig.SetFontPointSize((int)Font.Default.SizeInPoints+15);

            ITextBoxTextAttr taUnderlined2 = TextBox.CreateTextAttr();
            taUnderlined2.SetFontUnderlinedEx(
                TextBoxTextAttrUnderlineType.Special, 
                Color.Red);

            List<object> list = new()
            {
                "Text color is ", taTextColorRed, "red", taDefault, ".\n",
                "Background color is ", taBackColorYellow, "yellow", taDefault, ".\n",
                "Font is ", taUnderlined, "underlined", taDefault, ".\n",
                "Font is ", taBold, "bold", taDefault, ".\n",
                "Font is ", taItalic, "italic", taDefault, ".\n",
                "Font is ", taBig, "big", taDefault, ".\n",
                "Font is ", taUnderlined2, "special underlined", taDefault, ".\n",
                "This is url: ", taUrl, homePage, taDefault, ".\n",
            };

            multiLineTextBox.Clear();
            multiLineTextBox.AutoUrl = true;
            multiLineTextBox.IsRichEdit = true;

            multiLineTextBox.AppendTextAndStyles(list);
            multiLineTextBox.AppendNewLine();

            /*
            const string sUnorderedListItem = "Unordered List Item";
            const string sOrderedListItem = "Ordered List Item";
            
            for (int i = 1; i < 4; i++)
            {
                multiLineTextBox.SetDefaultStyle(taUnorderedList);
                multiLineTextBox.AppendText(sUnorderedListItem+i);
                multiLineTextBox.AppendNewLine();
            }
            multiLineTextBox.SetDefaultStyle(taDefault);
            multiLineTextBox.AppendNewLine();

            for (int i = 1; i < 4; i++)
            {
                multiLineTextBox.SetDefaultStyle(taOrderedList);
                multiLineTextBox.AppendText(sOrderedListItem + i + "\n");
                multiLineTextBox.SetDefaultStyle(taDefault);
                multiLineTextBox.AppendNewLine();
            }
            multiLineTextBox.SetDefaultStyle(taDefault);*/
        }
    }
}