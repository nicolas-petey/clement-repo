   M S B A M L   `   `   `  ����  TestCodecare    TestCodecare.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  
MainWindow��5   	   $	��500��5   	   $	��800��5	   	   .��5
      �  � RowDefinitions 5   
   �� 5      $��5 �6   �� 5      $��50 �6   �� 5      $��4* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $)��#/Helpers-NePasModifier/codecare.png��5       �� BitmapScalingMode$ HighQuality=�5      �� 5   
   $��Center=�6   $��1��5      $��20(�5      $
��Bold"�5      $��Center=�5      ���#006CC0��5      �� *
Libellé+�� 5   
   $��2��6   $
��10,0q�5      $��1q�5       ���#e0e0e0$��Stretch=�5"      $��Stretch=�5#       �� SelectedItem 5$      �� *Point+��5%      ��  �� Mode$ TwoWay=� �� Path$ Points� � 	Resources 5&         SelectedTemplate �� 
TargetType � ColumnDefinitions	 �� 
TargetType
 �� Property �� Value �� Triggers ]� 
Conditions �� Property �� Value��%-  &	        '�� (    7   �� 5'      " ��6;   .R�5(      � $��50��6   ���#006CC0 5)      �� 5*      $��* �6/   .+�5-      �� $��Center=�5.   $   $��Stretch=�5/   $   
�������$
��Bold"�51   $   $��16(�52   $   $��20,0,0,0q�53   $   ��5-   $   �� *.+53   $   54      55      �� 56      "	 ��6   .�57      �� 
 � ��$ Stretch=�6A   �� 58      
 � �� 59      �� 5:      " ��6.   .R�5;   "   � $��40��6'    5<   &   �� 5=   *   $��* �6;   �� 5>   *   $
��Auto �6;   .+�5A   &   �� $��Center=�5B   /   $��20,0,0,0q�5C   /   ��5A   0   �� *.+5C   /   5D   #   5E      5G       5H      ]� 5I       5J      �� 5K   "    �@��
IsSelected$
 true��6B   ./�5M      �� 
 � ��8   69   5N      5P      5Q      5R      � 5T   
   $��3��6    5U      �� 5V      �� 5W      .+�5Z      �� $��Center=�6   $��0q�5[      $	��200��5\      $��50��5]      $��Fermer��5_      ��5^      �� *FermerCommand+5_      5`      5a      5b      