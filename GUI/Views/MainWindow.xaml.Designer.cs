﻿//  *****************************************************************************
//  File:       MainWindow.Designer.cs
//  Solution:   ORM-Monitor
//  Project:    GUI
//  Date:       03/4/2017
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2017
//  *****************************************************************************

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ORM_Monitor.Models;

namespace ORM_Monitor.Views {
  public partial class MainWindow {
    private void DataBind_Columns<T>(ObservableCollection<T> collection) {
      foreach (var p in typeof(T).GetProperties()) {
        DataGridColumn dgc = null;

        switch (p.Name) {
          case "Action": {
            var btn = new FrameworkElementFactory(typeof(Button)) {
              Name = "ActionButton",
            };

            btn.SetResourceReference(TemplateProperty, "ButtonControlTemplate1");

            btn.SetValue(NameProperty, p.Name);
            btn.SetValue(BackgroundProperty, new ImageBrush((ImageSource) Application.Current.Resources["Button-Normal"]));
            btn.SetValue(MarginProperty, new Thickness(2));
            btn.SetValue(ForegroundProperty, new SolidColorBrush(Colors.Black));
            btn.SetValue(FontFamilyProperty, new FontFamily("Courier New"));
            btn.SetValue(FontSizeProperty, 12.0);
            btn.SetValue(FontWeightProperty, FontWeights.Bold);
            
            var dc = new DataGridTemplateColumn {
                CellStyle = new Style()
                {
                    Setters = {
                        new EventSetter {
                            Event = MouseEnterEvent,
                            Handler = new MouseEventHandler(ListView1_MouseEnter)
                        },
                        new EventSetter {
                            Event = MouseLeaveEvent,
                            Handler = new MouseEventHandler(ListView1_MouseLeave)
                        },
                        new EventSetter {
                            Event = MouseDownEvent,
                            Handler = new MouseButtonEventHandler(MyButton_MouseDown)
                        }
                    },
                    Triggers = {
                        new Trigger {
                            Property = IsMouseOverProperty,
                            Value = true,
                            Setters = {
                                new Setter(BackgroundProperty, Brushes.DarkSeaGreen)
                            }
                        }
                    }
                },
                Width = DataGridLength.SizeToCells,
                CanUserResize = false,
                CellTemplate = new DataTemplate {
                  VisualTree = btn
                }  
            };

            dgc = dc;
            break;
          }
          default: {
            var dc = new DataGridTextColumn {
              Binding = new Binding(p.Name),
              CellStyle = new Style() {
                Triggers = {
                  new Trigger {
                    Property = IsMouseOverProperty,
                    Value = true,
                    Setters = {
                      new Setter(BackgroundProperty, Brushes.DarkSeaGreen)
                    }
                  }
                }
              },
              ElementStyle = new Style() {
                Setters = {
                    new EventSetter {
                    Event = MouseEnterEvent,
                    Handler = new MouseEventHandler(ListView1_MouseEnter)
                  },
                  new EventSetter {
                    Event = MouseLeaveEvent,
                    Handler = new MouseEventHandler(ListView1_MouseLeave)
                  }
                }
              }
            };

            dc.ElementStyle.RegisterName(p.Name, dc);

            if (p.Name == "Progress") {
              dc.Binding.StringFormat = "{0}%";
            }

            dgc = dc;
            break;
          }
        }
        
        foreach (GridColumnAttribute attr in p.GetCustomAttributes(false).Where(s => p.Name != "Dispatcher")) {
          dgc.Header = attr.Header ?? p.Name;
          dgc.Width = attr.Width;
          dgc.MinWidth = dgc.Width.Value;
          dgc.Visibility = attr.Visibility;
          dgc.IsReadOnly = attr.IsReadOnly;

          if (dgc.Visibility == Visibility.Visible && p.Name != "DependencyObjectType" && p.Name != "IsSealed" && p.Name != "Dispatcher")
            ListView1.Columns.Add(dgc);
        }
      }
    }
  }
}
