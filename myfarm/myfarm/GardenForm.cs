using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myfarm
{
    public class GardenForm : Form
    {
        private IContainer components = null;
        private Timer timer;
        private Label moneyBalanceLabel;
        private List<Panel> panelsList = new List<Panel>();
        private Button buyNewBedButton;
        private int lastPanelBottomLineYCoordinate = -25;
        private void AddBedPanel(int bedId)
        {
            Bed currentBed = BedsManager.BedsArray[bedId];
            Label plantedItemNameLabel = new Label();
            Button gatherHarvestButton = new Button();
            Button seedShopButton = new Button();
            Panel bedPanel = new Panel();
            // 
            // plantedItemNameLabel properties
            // 
            plantedItemNameLabel.AutoSize = true;
            plantedItemNameLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            plantedItemNameLabel.Location = new Point(15, 15);
            plantedItemNameLabel.Name = "plantedItemNameLabel";
            plantedItemNameLabel.Size = new Size(79, 25);
            plantedItemNameLabel.TabIndex = 0;
            if (currentBed.PlantedItem == null)
            {
                plantedItemNameLabel.Text = "Empty here";
            }
            else
            {
                plantedItemNameLabel.Text = currentBed.PlantedItem.Name;
            }
            // 
            // gatherHarvestButton properties
            // 
            gatherHarvestButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            gatherHarvestButton.Location = new Point(120, 2);
            gatherHarvestButton.Name = "gatherHarvestButton";
            gatherHarvestButton.Size = new Size(180, 46);
            gatherHarvestButton.UseVisualStyleBackColor = true;
            gatherHarvestButton.TabIndex = 1;
            gatherHarvestButton.Click += new EventHandler((sender, e) => GatherHarvest(sender, e, currentBed));
            if (currentBed.TimeLeftBeforeRipening() == null)
            {
                gatherHarvestButton.Text = "Gather the harvest";
            }
            else
            {
                gatherHarvestButton.Text = currentBed.TimeLeftBeforeRipening() + " left";
                gatherHarvestButton.Enabled = false;
            }
            // 
            // seedShopButton properties
            // 
            seedShopButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            seedShopButton.Location = new Point(120, 2);
            seedShopButton.Name = "seedShopButton";
            seedShopButton.Size = new Size(180, 46);
            seedShopButton.UseVisualStyleBackColor = true;
            seedShopButton.TabIndex = 2;
            seedShopButton.Click += new EventHandler((sender, e) => ShowSeedShopForm(sender, e, currentBed));
            seedShopButton.Text = "Go to the seed shop";

            if (currentBed.PlantedItem == null)
            {
                gatherHarvestButton.Visible = false;
            }
            else
            {
                seedShopButton.Visible = false;
            }
            // 
            // panel
            //
            bedPanel.BackColor = SystemColors.ControlLightLight;
            bedPanel.BorderStyle = BorderStyle.FixedSingle;
            bedPanel.Location = new Point(12, lastPanelBottomLineYCoordinate + 55);
            lastPanelBottomLineYCoordinate += 55;
            bedPanel.Name = "bedPanel" + bedId;
            bedPanel.Size = new Size(305, 52);
            bedPanel.Controls.Add(plantedItemNameLabel);
            bedPanel.Controls.Add(gatherHarvestButton);
            bedPanel.Controls.Add(seedShopButton);
            Controls.Add(bedPanel);

            panelsList.Insert(bedId, bedPanel);
        }

        public GardenForm()
        {
            components = new Container();
            timer = new Timer(components);
            SuspendLayout();
            // 
            // timer
            // 
            timer.Interval = 10;
            timer.Tick += new EventHandler(TimerTick);
            // 
            // GardenForm
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "GardenForm";
            Text = "Garden";
            timer.Start();
            ResumeLayout(false);


            for (int i = 0; i < BedsManager.BedsArray.Count; i++)
            {
                AddBedPanel(i);
            }

            moneyBalanceLabel = new Label();
            moneyBalanceLabel.AutoSize = true;
            moneyBalanceLabel.Font = new Font("Bebas", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            moneyBalanceLabel.Location = new Point(8, 5);
            moneyBalanceLabel.Name = "moneyBalanceLabel";
            moneyBalanceLabel.Text = "Balance: " + EconomicsManager.AmountOfMoney + " $";
            moneyBalanceLabel.Size = new Size(79, 25);
            Controls.Add(moneyBalanceLabel);

            buyNewBedButton = new Button();
            buyNewBedButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            buyNewBedButton.Location = new Point(60, lastPanelBottomLineYCoordinate + 55);
            buyNewBedButton.Name = "buyNewBedButton";
            buyNewBedButton.Text = "Buy new bed - " + EconomicsManager.GetNewBedPrice() + " $";
            buyNewBedButton.Size = new Size(180, 46);
            buyNewBedButton.UseVisualStyleBackColor = true;
            buyNewBedButton.Click += new EventHandler(BuyNewBed);
            if (!EconomicsManager.IsPurchasePossible(EconomicsManager.GetNewBedPrice())) {
                buyNewBedButton.Enabled = false;
            }
            Controls.Add(buyNewBedButton);
        }

        void ShowSeedShopForm(object sender, EventArgs e, Bed currentBed)
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == "SeedShopForm" + currentBed.Id)
                {
                    MessageBox.Show("Seed shop for that bed is already opened.");
                    return;
                }
            }
            SeedShopForm seedShopForm = new SeedShopForm(currentBed);
            seedShopForm.Show();
        }

        void GatherHarvest(object sender, EventArgs e, Bed currentBed)
        {
            currentBed.SellRipedItem();
        }

        void BuyNewBed(object sender, EventArgs e) { 
            if (EconomicsManager.IsPurchasePossible(EconomicsManager.GetNewBedPrice()))
            {
                EconomicsManager.SubtractFromCurrentAmountOfMoney(EconomicsManager.GetNewBedPrice());
                BedsManager.AddNewBed();
            }
            else
            {
                MessageBox.Show("You don't have enough money.");
            }
        }

        private void TimerTick(object sender1, EventArgs e1)
        {
            moneyBalanceLabel.Text = "Balance: " + EconomicsManager.AmountOfMoney + " $";

            if (panelsList.Count < BedsManager.BedsArray.Count)
            {
                AddBedPanel(panelsList.Count);
                buyNewBedButton.Location = new Point(60, lastPanelBottomLineYCoordinate + 55);
                buyNewBedButton.Text = "Buy new bed - " + EconomicsManager.GetNewBedPrice() + " $";
            }

            if (EconomicsManager.IsPurchasePossible(EconomicsManager.GetNewBedPrice()))
            {
                buyNewBedButton.Enabled = true;
            }
            else
            {
                buyNewBedButton.Enabled = false;
            }

            for (int i = 0; i < panelsList.Count; i++) {
                Label plantedItemNameLabel = (Label)panelsList[i].Controls[0];
                Button gatherHarvestButton = (Button)panelsList[i].Controls[1];
                Button seedShopButton = (Button)panelsList[i].Controls[2];
                Bed currentBed = BedsManager.BedsArray[i];

                if (currentBed.PlantedItem == null)
                {
                    plantedItemNameLabel.Text = "Empty here";
                    gatherHarvestButton.Visible = false;
                    seedShopButton.Visible = true;                 
                }
                else
                {
                    plantedItemNameLabel.Text = currentBed.PlantedItem.Name;
                    if (currentBed.TimeLeftBeforeRipening() == null)
                    {
                        gatherHarvestButton.Text = "Gather the harvest";
                        gatherHarvestButton.Enabled = true;
                        gatherHarvestButton.Visible = true;
                        seedShopButton.Visible = false;
                    }
                    else
                    {
                        gatherHarvestButton.Text = currentBed.TimeLeftBeforeRipening() + " left";
                        gatherHarvestButton.Enabled = false;
                        gatherHarvestButton.Visible = true;
                        seedShopButton.Visible = false;
                    }
                }
            }
        }
    }
}
