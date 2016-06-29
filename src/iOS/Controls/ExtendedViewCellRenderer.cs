﻿using Bit.App.Controls;
using Bit.iOS.Controls;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]
namespace Bit.iOS.Controls
{
    public class ExtendedViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var extendedCell = (ExtendedViewCell)item;
            var cell = base.GetCell(item, reusableCell, tv);

            if(cell != null)
            {
                cell.BackgroundColor = extendedCell.BackgroundColor.ToUIColor();
                if(extendedCell.ShowDisclousure)
                {
                    cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                    if(!string.IsNullOrEmpty(extendedCell.DisclousureImage))
                    {
                        var detailDisclosureButton = UIButton.FromType(UIButtonType.Custom);
                        detailDisclosureButton.SetImage(UIImage.FromBundle(extendedCell.DisclousureImage), UIControlState.Normal);

                        try
                        {
                            detailDisclosureButton.SetImage(UIImage.FromBundle(extendedCell.DisclousureImage + "_selected"), UIControlState.Selected);
                        }
                        catch
                        {
                            detailDisclosureButton.SetImage(UIImage.FromBundle(extendedCell.DisclousureImage), UIControlState.Selected);
                        }

                        detailDisclosureButton.Frame = new CGRect(0f, 0f, 50f, 40f);
                        detailDisclosureButton.TouchUpInside += (sender, e) =>
                        {
                            extendedCell.OnDisclousureTapped();
                        };
                        cell.AccessoryView = detailDisclosureButton;
                    }
                }
            }

            return cell;
        }
    }
}
