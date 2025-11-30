namespace PocketPiggy
{
    partial class Questionnaire
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Questionnaire));
            name = new Label();
            nameTB = new TextBox();
            age = new Label();
            birthMonth = new ComboBox();
            ageTB = new TextBox();
            birthday = new Label();
            birthDate = new ComboBox();
            birthYear = new ComboBox();
            gender = new Label();
            genderCB = new ComboBox();
            genderCustomTB = new TextBox();
            occupation = new Label();
            sourceOfIncome = new Label();
            occupationCB = new ComboBox();
            sourceOfIncomeCB = new ComboBox();
            maritalStatus = new Label();
            maritalStatusCB = new ComboBox();
            averageIncome = new Label();
            averageIncomeCB = new ComboBox();
            monthlySpend = new Label();
            monthlySpendCB = new ComboBox();
            expense = new Label();
            financialGoal = new Label();
            save = new Label();
            confidence = new Label();
            reminders = new Label();
            expenseCB = new ComboBox();
            financialGoalCB = new ComboBox();
            saveCB = new ComboBox();
            confidenceCB = new ComboBox();
            remindersCB = new ComboBox();
            submitbtn = new Button();
            SuspendLayout();
            // 
            // name
            // 
            name.AutoSize = true;
            name.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            name.Location = new Point(31, 35);
            name.Margin = new Padding(2, 0, 2, 0);
            name.Name = "name";
            name.Size = new Size(487, 30);
            name.TabIndex = 0;
            name.Text = "Full Name (Last Name, First Name, Middle Initial):";
            // 
            // nameTB
            // 
            nameTB.BorderStyle = BorderStyle.FixedSingle;
            nameTB.Location = new Point(31, 84);
            nameTB.Margin = new Padding(2);
            nameTB.Name = "nameTB";
            nameTB.Size = new Size(958, 31);
            nameTB.TabIndex = 1;
            // 
            // age
            // 
            age.AutoSize = true;
            age.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            age.Location = new Point(31, 148);
            age.Margin = new Padding(2, 0, 2, 0);
            age.Name = "age";
            age.Size = new Size(57, 30);
            age.TabIndex = 2;
            age.Text = "Age:";
            // 
            // birthMonth
            // 
            birthMonth.BackColor = Color.LightPink;
            birthMonth.DropDownStyle = ComboBoxStyle.DropDownList;
            birthMonth.FlatStyle = FlatStyle.Popup;
            birthMonth.FormattingEnabled = true;
            birthMonth.Location = new Point(294, 185);
            birthMonth.Margin = new Padding(2);
            birthMonth.Name = "birthMonth";
            birthMonth.Size = new Size(134, 33);
            birthMonth.TabIndex = 3;
            // 
            // ageTB
            // 
            ageTB.BorderStyle = BorderStyle.FixedSingle;
            ageTB.Location = new Point(31, 185);
            ageTB.Margin = new Padding(2);
            ageTB.Name = "ageTB";
            ageTB.Size = new Size(94, 31);
            ageTB.TabIndex = 4;
            // 
            // birthday
            // 
            birthday.AutoSize = true;
            birthday.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            birthday.Location = new Point(294, 148);
            birthday.Margin = new Padding(2, 0, 2, 0);
            birthday.Name = "birthday";
            birthday.Size = new Size(98, 30);
            birthday.TabIndex = 5;
            birthday.Text = "Birthday:";
            // 
            // birthDate
            // 
            birthDate.BackColor = Color.LightPink;
            birthDate.DropDownStyle = ComboBoxStyle.DropDownList;
            birthDate.FlatStyle = FlatStyle.Popup;
            birthDate.FormattingEnabled = true;
            birthDate.Location = new Point(434, 184);
            birthDate.Margin = new Padding(2);
            birthDate.MaxDropDownItems = 12;
            birthDate.Name = "birthDate";
            birthDate.Size = new Size(92, 33);
            birthDate.TabIndex = 6;
            // 
            // birthYear
            // 
            birthYear.BackColor = Color.LightPink;
            birthYear.DropDownStyle = ComboBoxStyle.DropDownList;
            birthYear.FlatStyle = FlatStyle.Popup;
            birthYear.FormattingEnabled = true;
            birthYear.Location = new Point(531, 185);
            birthYear.Margin = new Padding(2);
            birthYear.MaxDropDownItems = 12;
            birthYear.Name = "birthYear";
            birthYear.Size = new Size(122, 33);
            birthYear.TabIndex = 7;
            // 
            // gender
            // 
            gender.AutoSize = true;
            gender.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gender.Location = new Point(750, 148);
            gender.Margin = new Padding(2, 0, 2, 0);
            gender.Name = "gender";
            gender.Size = new Size(90, 30);
            gender.TabIndex = 8;
            gender.Text = "Gender:";
            // 
            // genderCB
            // 
            genderCB.BackColor = Color.LightPink;
            genderCB.DropDownStyle = ComboBoxStyle.DropDownList;
            genderCB.FlatStyle = FlatStyle.Popup;
            genderCB.FormattingEnabled = true;
            genderCB.Location = new Point(750, 186);
            genderCB.Margin = new Padding(2);
            genderCB.Name = "genderCB";
            genderCB.Size = new Size(146, 33);
            genderCB.TabIndex = 9;
            // 
            // genderCustomTB
            // 
            genderCustomTB.BorderStyle = BorderStyle.FixedSingle;
            genderCustomTB.Location = new Point(750, 226);
            genderCustomTB.Margin = new Padding(2);
            genderCustomTB.Name = "genderCustomTB";
            genderCustomTB.PlaceholderText = "Please Specify...";
            genderCustomTB.ShortcutsEnabled = false;
            genderCustomTB.Size = new Size(147, 31);
            genderCustomTB.TabIndex = 10;
            // 
            // occupation
            // 
            occupation.AutoSize = true;
            occupation.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            occupation.Location = new Point(31, 286);
            occupation.Margin = new Padding(2, 0, 2, 0);
            occupation.Name = "occupation";
            occupation.Size = new Size(128, 30);
            occupation.TabIndex = 10;
            occupation.Text = "Occupation:";
            // 
            // sourceOfIncome
            // 
            sourceOfIncome.AutoSize = true;
            sourceOfIncome.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            sourceOfIncome.Location = new Point(382, 286);
            sourceOfIncome.Margin = new Padding(2, 0, 2, 0);
            sourceOfIncome.Name = "sourceOfIncome";
            sourceOfIncome.Size = new Size(189, 30);
            sourceOfIncome.TabIndex = 12;
            sourceOfIncome.Text = "Source of Income:";
            // 
            // occupationCB
            // 
            occupationCB.BackColor = Color.LightPink;
            occupationCB.DropDownStyle = ComboBoxStyle.DropDownList;
            occupationCB.FlatStyle = FlatStyle.Popup;
            occupationCB.FormattingEnabled = true;
            occupationCB.Location = new Point(31, 332);
            occupationCB.Margin = new Padding(2);
            occupationCB.Name = "occupationCB";
            occupationCB.Size = new Size(229, 33);
            occupationCB.TabIndex = 13;
            // 
            // sourceOfIncomeCB
            // 
            sourceOfIncomeCB.BackColor = Color.LightPink;
            sourceOfIncomeCB.DropDownStyle = ComboBoxStyle.DropDownList;
            sourceOfIncomeCB.FlatStyle = FlatStyle.Popup;
            sourceOfIncomeCB.FormattingEnabled = true;
            sourceOfIncomeCB.Location = new Point(382, 332);
            sourceOfIncomeCB.Margin = new Padding(2);
            sourceOfIncomeCB.Name = "sourceOfIncomeCB";
            sourceOfIncomeCB.Size = new Size(304, 33);
            sourceOfIncomeCB.TabIndex = 14;
            // 
            // maritalStatus
            // 
            maritalStatus.AutoSize = true;
            maritalStatus.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            maritalStatus.Location = new Point(802, 286);
            maritalStatus.Margin = new Padding(2, 0, 2, 0);
            maritalStatus.Name = "maritalStatus";
            maritalStatus.Size = new Size(148, 30);
            maritalStatus.TabIndex = 15;
            maritalStatus.Text = "Marital Status:";
            // 
            // maritalStatusCB
            // 
            maritalStatusCB.BackColor = Color.LightPink;
            maritalStatusCB.DropDownStyle = ComboBoxStyle.DropDownList;
            maritalStatusCB.FlatStyle = FlatStyle.Popup;
            maritalStatusCB.FormattingEnabled = true;
            maritalStatusCB.Location = new Point(802, 332);
            maritalStatusCB.Margin = new Padding(2);
            maritalStatusCB.Name = "maritalStatusCB";
            maritalStatusCB.Size = new Size(146, 33);
            maritalStatusCB.TabIndex = 16;
            // 
            // averageIncome
            // 
            averageIncome.AutoSize = true;
            averageIncome.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            averageIncome.Location = new Point(31, 402);
            averageIncome.Margin = new Padding(2, 0, 2, 0);
            averageIncome.Name = "averageIncome";
            averageIncome.Size = new Size(305, 30);
            averageIncome.TabIndex = 17;
            averageIncome.Text = "What is your average income?";
            // 
            // averageIncomeCB
            // 
            averageIncomeCB.BackColor = Color.LightPink;
            averageIncomeCB.DropDownStyle = ComboBoxStyle.DropDownList;
            averageIncomeCB.FlatStyle = FlatStyle.Popup;
            averageIncomeCB.FormattingEnabled = true;
            averageIncomeCB.Location = new Point(34, 451);
            averageIncomeCB.Margin = new Padding(2);
            averageIncomeCB.Name = "averageIncomeCB";
            averageIncomeCB.Size = new Size(394, 33);
            averageIncomeCB.TabIndex = 18;
            // 
            // monthlySpend
            // 
            monthlySpend.AutoSize = true;
            monthlySpend.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            monthlySpend.Location = new Point(526, 402);
            monthlySpend.Margin = new Padding(2, 0, 2, 0);
            monthlySpend.Name = "monthlySpend";
            monthlySpend.Size = new Size(471, 30);
            monthlySpend.TabIndex = 19;
            monthlySpend.Text = "On average, how much do you spend monthly?";
            // 
            // monthlySpendCB
            // 
            monthlySpendCB.BackColor = Color.LightPink;
            monthlySpendCB.DropDownStyle = ComboBoxStyle.DropDownList;
            monthlySpendCB.FlatStyle = FlatStyle.Popup;
            monthlySpendCB.FormattingEnabled = true;
            monthlySpendCB.Location = new Point(531, 451);
            monthlySpendCB.Margin = new Padding(2);
            monthlySpendCB.Name = "monthlySpendCB";
            monthlySpendCB.Size = new Size(394, 33);
            monthlySpendCB.TabIndex = 20;
            // 
            // expense
            // 
            expense.AutoSize = true;
            expense.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            expense.Location = new Point(31, 516);
            expense.Margin = new Padding(2, 0, 2, 0);
            expense.Name = "expense";
            expense.Size = new Size(397, 30);
            expense.TabIndex = 21;
            expense.Text = "How often do you track your expenses?";
            // 
            // financialGoal
            // 
            financialGoal.AutoSize = true;
            financialGoal.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            financialGoal.Location = new Point(526, 516);
            financialGoal.Margin = new Padding(2, 0, 2, 0);
            financialGoal.Name = "financialGoal";
            financialGoal.Size = new Size(427, 30);
            financialGoal.TabIndex = 22;
            financialGoal.Text = "What is your main financial goal right now?";
            // 
            // save
            // 
            save.AutoSize = true;
            save.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            save.Location = new Point(31, 618);
            save.Margin = new Padding(2, 0, 2, 0);
            save.Name = "save";
            save.Size = new Size(436, 30);
            save.TabIndex = 23;
            save.Text = "How much do you want to save per month?";
            // 
            // confidence
            // 
            confidence.AutoSize = true;
            confidence.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            confidence.Location = new Point(526, 618);
            confidence.Margin = new Padding(2, 0, 2, 0);
            confidence.Name = "confidence";
            confidence.Size = new Size(492, 30);
            confidence.TabIndex = 24;
            confidence.Text = "How confident are you in managing your money?";
            // 
            // reminders
            // 
            reminders.AutoSize = true;
            reminders.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            reminders.Location = new Point(31, 720);
            reminders.Margin = new Padding(2, 0, 2, 0);
            reminders.Name = "reminders";
            reminders.Size = new Size(706, 30);
            reminders.TabIndex = 25;
            reminders.Text = "How frequently would you like to receive budget reminders or insights?";
            // 
            // expenseCB
            // 
            expenseCB.BackColor = Color.LightPink;
            expenseCB.DropDownStyle = ComboBoxStyle.DropDownList;
            expenseCB.FlatStyle = FlatStyle.Popup;
            expenseCB.FormattingEnabled = true;
            expenseCB.Location = new Point(31, 565);
            expenseCB.Margin = new Padding(2);
            expenseCB.Name = "expenseCB";
            expenseCB.Size = new Size(394, 33);
            expenseCB.TabIndex = 26;
            // 
            // financialGoalCB
            // 
            financialGoalCB.BackColor = Color.LightPink;
            financialGoalCB.DropDownStyle = ComboBoxStyle.DropDownList;
            financialGoalCB.FlatStyle = FlatStyle.Popup;
            financialGoalCB.FormattingEnabled = true;
            financialGoalCB.Location = new Point(542, 565);
            financialGoalCB.Margin = new Padding(2);
            financialGoalCB.Name = "financialGoalCB";
            financialGoalCB.Size = new Size(394, 33);
            financialGoalCB.TabIndex = 27;
            // 
            // saveCB
            // 
            saveCB.BackColor = Color.LightPink;
            saveCB.DropDownStyle = ComboBoxStyle.DropDownList;
            saveCB.FlatStyle = FlatStyle.Popup;
            saveCB.FormattingEnabled = true;
            saveCB.Location = new Point(34, 671);
            saveCB.Margin = new Padding(2);
            saveCB.Name = "saveCB";
            saveCB.Size = new Size(394, 33);
            saveCB.TabIndex = 28;
            // 
            // confidenceCB
            // 
            confidenceCB.BackColor = Color.LightPink;
            confidenceCB.DropDownStyle = ComboBoxStyle.DropDownList;
            confidenceCB.FlatStyle = FlatStyle.Popup;
            confidenceCB.FormattingEnabled = true;
            confidenceCB.Location = new Point(542, 671);
            confidenceCB.Margin = new Padding(2);
            confidenceCB.Name = "confidenceCB";
            confidenceCB.Size = new Size(394, 33);
            confidenceCB.TabIndex = 29;
            // 
            // remindersCB
            // 
            remindersCB.BackColor = Color.LightPink;
            remindersCB.DropDownStyle = ComboBoxStyle.DropDownList;
            remindersCB.FlatStyle = FlatStyle.Popup;
            remindersCB.FormattingEnabled = true;
            remindersCB.Location = new Point(34, 775);
            remindersCB.Margin = new Padding(2);
            remindersCB.Name = "remindersCB";
            remindersCB.Size = new Size(394, 33);
            remindersCB.TabIndex = 30;
            // 
            // submitbtn
            // 
            submitbtn.BackColor = Color.LightPink;
            submitbtn.FlatStyle = FlatStyle.Popup;
            submitbtn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            submitbtn.Location = new Point(740, 760);
            submitbtn.Margin = new Padding(4, 4, 4, 4);
            submitbtn.Name = "submitbtn";
            submitbtn.Size = new Size(250, 50);
            submitbtn.TabIndex = 0;
            submitbtn.Text = "Submit";
            submitbtn.UseVisualStyleBackColor = false;
            submitbtn.Click += SubmitBtn_Click;
            // 
            // Questionnaire
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMinSize = new Size(1114, 730);
            BackColor = Color.LavenderBlush;
            ClientSize = new Size(1090, 850);
            Controls.Add(submitbtn);
            Controls.Add(remindersCB);
            Controls.Add(confidenceCB);
            Controls.Add(saveCB);
            Controls.Add(financialGoalCB);
            Controls.Add(expenseCB);
            Controls.Add(reminders);
            Controls.Add(confidence);
            Controls.Add(save);
            Controls.Add(financialGoal);
            Controls.Add(expense);
            Controls.Add(monthlySpendCB);
            Controls.Add(monthlySpend);
            Controls.Add(averageIncomeCB);
            Controls.Add(averageIncome);
            Controls.Add(maritalStatusCB);
            Controls.Add(maritalStatus);
            Controls.Add(sourceOfIncomeCB);
            Controls.Add(occupationCB);
            Controls.Add(sourceOfIncome);
            Controls.Add(occupation);
            Controls.Add(genderCustomTB);
            Controls.Add(genderCB);
            Controls.Add(gender);
            Controls.Add(birthYear);
            Controls.Add(birthDate);
            Controls.Add(birthday);
            Controls.Add(ageTB);
            Controls.Add(birthMonth);
            Controls.Add(age);
            Controls.Add(nameTB);
            Controls.Add(name);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "Questionnaire";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Questionnaire";
            Load += Questionnaire_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion



        private Label name;
        private TextBox nameTB;
        private Label age;
        private ComboBox birthMonth;
        private TextBox ageTB;
        private Label birthday;
        private ComboBox birthDate;
        private ComboBox birthYear;
        private Label gender;
        private ComboBox genderCB;
        private TextBox genderCustomTB;
        private Label occupation;
        private Label sourceOfIncome;
        private ComboBox occupationCB;
        private ComboBox sourceOfIncomeCB;
        private Label maritalStatus;
        private ComboBox maritalStatusCB;
        private Label averageIncome;
        private ComboBox averageIncomeCB;
        private Label monthlySpend;
        private ComboBox monthlySpendCB;
        private Label expense;
        private Label financialGoal;
        private Label save;
        private Label confidence;
        private Label reminders;
        private ComboBox expenseCB;
        private ComboBox financialGoalCB;
        private ComboBox saveCB;
        private ComboBox confidenceCB;
        private ComboBox remindersCB;
        private Button submitbtn;
    }
}


