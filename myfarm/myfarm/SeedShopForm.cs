using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace myfarm
{
    public partial class SeedShopForm : Form
    {
        private IContainer components = null;
        private Timer timer;
        private Bed currentBed;
        public Bed CurrentBed { set => currentBed = value; }
        private List<Panel> panelsList = new List<Panel>();
        private int lastPanelBottomLineYCoordinate = -90;
        public SeedShopForm(Bed currentBed)
        {
            this.currentBed = currentBed;
            components = new Container();
            timer = new Timer(components);
            SuspendLayout();
            // 
            // timer
            // 
            timer.Interval = 10;
            timer.Tick += new EventHandler(TimerTick);
            SuspendLayout();
            // 
            // SeedShopForm
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "SeedShopForm" + currentBed.Id;
            Text = "Seed Shop - Bed #" + currentBed.Id;
            timer.Start();
            ResumeLayout(false);

            for (int i = 0; i < PlantsManager.PlantsArray.Count; i++)
            {
                Plant currentItem = PlantsManager.PlantsArray[i];
                Label itemNameLabel = new Label();
                Label itemSeedPriceLabel = new Label();
                Label itemSellingPriceLabel = new Label();
                Label itemTimeToRipenLabel = new Label();
                Button plantItemButton = new Button();
                Panel itemSpecsPanel = new Panel();
                // 
                // itemNameLabel properties
                // 
                itemNameLabel.AutoSize = true;
                itemNameLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                itemNameLabel.Location = new Point(5, 5);
                itemNameLabel.Name = "itemNameLabel";
                itemNameLabel.Size = new Size(79, 25);
                itemNameLabel.Text = "Name: " + PlantsManager.PlantsArray[i].Name;
                itemNameLabel.TabIndex = 0;
                // 
                // itemPriceLabel properties
                // 
                itemSeedPriceLabel.AutoSize = true;
                itemSeedPriceLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                itemSeedPriceLabel.Location = new Point(5, 25);
                itemSeedPriceLabel.Name = "itemSeedPriceLabel";
                itemSeedPriceLabel.Size = new Size(79, 25);
                itemSeedPriceLabel.Text = "Seed price: " + PlantsManager.PlantsArray[i].SeedPrice + " $";
                itemSeedPriceLabel.TabIndex = 1;
                // 
                // itemSellingPriceLabel
                // 
                itemSellingPriceLabel.AutoSize = true;
                itemSellingPriceLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                itemSellingPriceLabel.Location = new Point(5, 45);
                itemSellingPriceLabel.Name = "itemSellingPriceLabel";
                itemSellingPriceLabel.Size = new Size(79, 25);
                itemSellingPriceLabel.Text = "Selling price: " + PlantsManager.PlantsArray[i].SellingPrice + " $";
                itemSellingPriceLabel.TabIndex = 2;
                // 
                // itemTimeToRipenLabel
                // 
                itemTimeToRipenLabel.AutoSize = true;
                itemTimeToRipenLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                itemTimeToRipenLabel.Location = new Point(5, 65);
                itemTimeToRipenLabel.Name = "itemTimeToRipenLabel";
                itemTimeToRipenLabel.Size = new Size(79, 25);
                itemTimeToRipenLabel.Text = "Time to ripen: " + PlantsManager.PlantsArray[i].TimeToRipen + " s";
                itemTimeToRipenLabel.TabIndex = 3;
                // 
                // plantItemButton properties
                // 
                plantItemButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                plantItemButton.Location = new Point(150, 5);
                plantItemButton.Name = "plantItemButton";
                plantItemButton.Size = new Size(175, 75);
                plantItemButton.UseVisualStyleBackColor = true;
                plantItemButton.Text = "Plant";
                plantItemButton.TabIndex = 4;
                if (!EconomicsManager.IsPurchasePossible(currentItem.SeedPrice))
                {
                    plantItemButton.Enabled = false;
                }
                plantItemButton.Click += new EventHandler((sender, e) => PlantItem(sender, e, currentItem));
                // 
                // panel
                // 
                itemSpecsPanel.BackColor = SystemColors.ControlLightLight;
                itemSpecsPanel.BorderStyle = BorderStyle.FixedSingle;
                itemSpecsPanel.Controls.Add(itemNameLabel);
                itemSpecsPanel.Controls.Add(itemSeedPriceLabel);
                itemSpecsPanel.Controls.Add(itemSellingPriceLabel);
                itemSpecsPanel.Controls.Add(itemTimeToRipenLabel);
                itemSpecsPanel.Controls.Add(plantItemButton);
                itemSpecsPanel.Location = new Point(12, lastPanelBottomLineYCoordinate + 95);
                lastPanelBottomLineYCoordinate += 95;
                itemSpecsPanel.Name = "itemSpecsPanel";
                itemSpecsPanel.Size = new Size(330, 90);
                itemSpecsPanel.Visible = true;
                Controls.Add(itemSpecsPanel);
                panelsList.Insert(i, itemSpecsPanel);
                PerformLayout();
            }

            FormClosing += new FormClosingEventHandler(StopTimerOnFormClose);
        }

        private void StopTimerOnFormClose(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            for (int i = 0; i < panelsList.Count; i++)
            {
                Button plantItemButton = (Button)panelsList[i].Controls[4];
                if (EconomicsManager.IsPurchasePossible(PlantsManager.PlantsArray[i].SeedPrice))
                {
                    plantItemButton.Enabled = true;
                }
                else
                {
                    plantItemButton.Enabled = false;
                }
            }
        }

        void PlantItem(object sender, EventArgs e, Plant selectedItem)
        {
            if (EconomicsManager.IsPurchasePossible(selectedItem.SeedPrice))
            {
                currentBed.PlantItem(selectedItem);
                Close();
            }
            else
            {
                MessageBox.Show("You don't have enough money for this.");
            }
        }
    }
}
