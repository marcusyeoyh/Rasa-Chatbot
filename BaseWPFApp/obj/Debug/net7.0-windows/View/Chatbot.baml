   M S B A M L   `   `   `  ����  
BaseWPFApp    BaseWPFApp.View.Chatbot   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Chatbot��5   	   $	��600��6   $	��800��6&   ���	#FFEAEAEA���	#FF151515$��14(�6`    � WindowStyle$ ThreeDBorderWindow=�6n    � WindowStartupLocation$ CenterScreen=�6�   $��Segoe UI)�6�    � 	Resources 5       �� 
TargetType   UserMessageBubbleStyle �� Property �� Value  BotMessageBubbleStyle  ProductButtonStyle �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value��%�  &	        &	     &	 3    �� 5	   
   " ��6   .�5
      ��  � �� �#007AFF6+   �� 5       � ��$	 400��6)   �� 5       � ��$ 5q�6'   �� 5       �@��Padding$ 10q�6(   �� 5       �@��CornerRadius$ 10��6-   �� 5       � ��$ Right=�64   5      �� 5   
   " ��6   .�5      ��  � �� �#34C7596+   �� 5       � ��$	 400��6)   �� 5       � ��$ 5q�6'   �� 5       �@��Padding$ 10q�6(   �� 5       �@��CornerRadius$ 10��6-   �� 5       � ��$
 Left=�64   5      �� 5   
   " ��6+   .�5      ��  � �� �#6C757D6+   �� 5       � ��
 �����6+   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ 0,5,0,0q�6'   �� 5        � ��$ 0q�60   �� 5!       � ��$ 14(�6)   �� 5"       �@�Cursor$
 Hand��6'   �� 5#       � ��$	 120��6&   �� 5%       � �� 5&      �� 5'      " ��6&   .R�5(      �� #��z��6!   #��z��5)      #��z��5*      $ 5��5+      ._�5,      �� $��Center=�6/   $��Center=�5-   '   5.      5/      51      	 52      P� 53      
 �@J�IsMouseOver$
 True��61   . �54      ��  � �� �#49505763   55      57      59      .��5:      �  � RowDefinitions 5;   
   �� 5<      $��* �6   �� 5=      $
��Auto �6   .+�5@   
   �� -   5         ��Name$ ResultsScroller��5@      $��0��60   $
��Auto=�6=   $��0,0,0,10q�6`   .��5A      �� -   5         ��Name$ ResultsPanel��5A      $��10q�6/   5B      �� 5D   
   $��1��6   $
��0,10q�6"   .+�5E      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $��0,0,0,5q�6O   .+�5F      �� -   5         ��Name$ txtInput��5F      $��35��6,   $	��600��68   $��5,5,5,5q�6D   $��Enter query here ...��6V   6�   �� 5G      -   5         ��Name$ btnGo��5G      $��80��6(   $��5,5,5,5q�63   $
��Send��6E   6T   5H      5I      5J      5K      