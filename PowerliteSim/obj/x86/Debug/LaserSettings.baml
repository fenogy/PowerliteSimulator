   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      G CPowerliteSim, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:System.Windows.ControlsSystem.Windows.Controls 5   �       PowerliteSim.LaserSettings   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      6sys&clr-namespace:System;assembly=mscorlib     5      4my%clr-namespace:System.Windows.Controls     5   �     � Title$  LaserSettings��6    � 
ResizeMode$ CanMinimize=�6,   $	��360��6E   $	��400��6T    � WindowStyle$
 None=�6d    � AllowsTransparency .  � SizeToContent$ WidthAndHeight=�6�    � WindowStartupLocation$ CenterScreen=�6  ��5      �� 5	   
   .��5      � $	��373��6    � 	Resources 5   
    	  brush 
� GradientOrigin 
� Center	 
� RadiusX
 
� RadiusY � Color � Offset��%�   &	        
� 5      $	 0,0&�60   $ 0.5,0.5&�6E   $		 1.2Q�6V   $	
 1.2Q�6d   ��5      � $ white��6#   $ 0Q�61   � 5      $
 gray��6#   $ 1Q�60   5      5       � RowDefinitions 5   
   �� 5      $
��217* �6   �� 5      $
��156* �6   .+�5   
   �� -   5        $��
rectangle1��5   ~   $��2��6   $	��334��6%   #��[  62   $
��Left=�6P   $��	4,-14,0,0q�6k   
���   �$	��Top=�6�   $	��328��6�    �� RadiusY$ 10��6�    �� RadiusX$ 10��6�   $��2��6�    J� Opacity$
 0.85Q�6  �� 5   
   -   5        $��button2��5   ]   $��Cancel��6   $��23��6"   $
��Left=�6.   $��
246,73,0,0q�6I   $	��Top=�6l   $��72��6�   $��1��6�   �� 5   
   -   5        $��button1��5   Y   $��OK��6   $��23��6   $
��Left=�6*   $��
158,73,0,0q�6E   $	��Top=�6h   $��65��6�   $��1��6�   -  'System.Windows.Controls.WpfPropertyGrid  5   
   -   5          Name$ propertyGrid��5   X   $	��259��6   $
��Left=�6*   $��	18,22,0,0q�6E   $	��Top=�6n   $	��300��6�   $��2��6�   ���	#FF9DC1FF J� OpacityMask
 �   �$	 0.9Q�6�   �� 5   
   -   5        $��label1��5   c   $��Laser Settings��6   $��26��6)   $
��Left=�65   $��	18,-8,0,0q�6P   $	��Top=�6q   $	��175��6�   $��16(�6�   $��
Wintermute)�6�   $��Normal"�6�   6�   �� 5   
   -   5        $��
rectangle2��5   O   $	��261��6   $
��Left=�6!   $��	17,21,0,0q�6<   
���   �$	��Top=�6p   $	��302��6�   $��2��6�   5      5       