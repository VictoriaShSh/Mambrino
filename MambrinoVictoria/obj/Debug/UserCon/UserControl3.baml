   M S B A M L   `   `   `  ����K  GMambrinoVictoria, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MambrinoVictoria.UserConMambrinoVictoria.UserCon  5      +    %MambrinoVictoria.UserCon.UserControl3   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:MambrinoVictoria.UserCon     5        � 	Resources  5
       �� Source�� 5   
   $! ../HojaEstilos/Estilos.xaml?�6   %    .��5      �  � ColumnDefinitions 5   
   �� 5      $	��20* �6   �� 5      $
��600* �6   �� 5      $
��130* �6    � RowDefinitions 5   
   �� 5      $
��200* �6   �� 5      $��40 �6   .+�5   
   �� $��0��6   $��0��6!   ���	#FF003DF961   �� 5   
   $��1��6   $��0��6!   ���	#FF003DF9$��3��6B   �� 5   
   
�������$��1q�6%   $��1��69   $��0��6I   $��2��6V   �� 5   
   
�������$��1q�6%   $��1��69   $��1��6I   $��2��6V   �� 5   
   
�������$��1q�6%   $��0��69   $��0��6I   $��2��6V   �� 5    
   -   5         ��Name$ lista��5       $��0��6"   $��1��6/   $��2��6?   ���	#FFCCECF1$��1,1,1,1q�6j   $��Arial)�6{   $
��Bold"�6�   $��18(�6�    �� View 5!      �� 5"      ! �� ColumnHeaderContainerStyle 5#      �� 5$       �� 
TargetType" ��6    .�5%      ��  �� Property � ��	 �� Value	 �	#FF003DF96;   �� 5&       � ��
	 �����6;   �� 5'       � ��$	 Center=�6K   �� 5(       � ��
	 �����6<   �� 5)       � ��$	 2q�6@   5*      .P�5,      �� $	��Nº��6%   
 �� Width$
 60��6V    �� DisplayMemberBinding 61   �� *[0]+6V   �� 5-      $��Fecha Inicio Sintomas��6%   $	
 200��6i    6D   �� *[1]+6i   �� 5.      $��Fecha Diagnostico��6%   $	
 200��6e    6@   �� *[2]+6e   �� 5/      $��Sintomas��6%   $	
 250��6\    67   �� *[3]+6\   �� 50      $��Diagnostico��6%   $	
 250��6_    6:   �� *[4]+6_   �� 51      $��Especialidad��6%   $	
 150��6`    6;   �� *[5]+6`   �� 52      $��Codificacion��6%   $	
 120��6`    6;   �� *[6]+6`   53      55      �� 57   
   -   5         ��Name$ añadir��57   7      BotonSimple#���   6   $��Añadir��6G   $��1��6X   $��1��6h   $��Right=�6u   6�   �� 58   
   -   5        $ cancelar��58   7   #���   6   $��Cancelar��6I   $��2��6\   $��1��6l   $��25,3,5,3q�6y   6�   59      5:      