   M S B A M L   `   `   `  ����K  GMambrinoVictoria, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:MambrinoVictoria.ProgramaMambrinoVictoria.Programa  5   	   0    *MambrinoVictoria.Programa.HojaPrescripcion   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:MambrinoVictoria.Programa     5   	   $	��670��5   &   $	��300��65     � 	Resources  5
       �� Source�� 5   
   $! ../HojaEstilos/Estilos.xaml?�6   %    .��5      � ���	#FFCCECF1 � ColumnDefinitions 5   
   �� 5      $��10 �6   �� 5      $	��250 �6   �� 5      $	��200 �6   �� 5      $	��180 �6   �� 5      $��10 �6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��50 �6   �� 5      $��30 �6   �� 5      $��40 �6   �� 5      $��50 �6   �� 5      $��50 �6   .+�5   
   �� $��1��6   $��1��6!   $��3��61   ���	#FF003DF96E   �� 5    
   $��4��6   $��1��6!   $��3��61   ���	#FF003DF96E   �� 5!   
   $��0��6   $��1��6!   ���	#FF003DF9$��3��6B   �� 5"   
   $��3��6   $��1��6!   ���	#FF003DF9$��3��6B   �� 5$   
   
�������$��2q�6%   $��1��69   $��0��6I   �� 5%   
   
�������$��2q�6%   $��2��69   $��0��6I   �� 5&   
   
�������$��2q�6%   $��3��69   $��0��6I   �� 5'   
   
�������$��2q�6%   $��1��69   $��1��6I   �� 5(   
   
�������$��2q�6%   $��2��69   $��1��6I   �� 5)   
   
�������$��2q�6%   $��3��69   $��1��6I   �� 5+   
   
�������$��2q�6%   $��1��69   $��3��6I   �� 5,   
   
�������$��2q�6%   $��2��69   $��3��6I   �� 5-   
   
�������$��2q�6%   $��3��69   $��3��6I   �� 5.   
   
�������$��2q�6%   $��1��69   $��4��6I   �� 5/   
   
�������$��2q�6%   $��2��69   $��4��6I   �� 50   
   
�������$��2q�6%   $��3��69   $��4��6I   �� 52   
      TextoBlanco1#���   6   $��1��67   $��0��6G   $��Especialidad��6T   �� 53   
   #���   6   $��2��67   $��0��6G   $��Principio Activo��6T   �� 54   
   #���   6   $��3��67   $��0��6G   $��Dosis��6T   �� 55   
   #���   6   $��1��67   $��3��6G   $
��Vía��6T   �� 56   
   #���   6   $��2��67   $��3��6G   $��
Frecuencia��6T   �� 57   
   #���   6   $��3��67   $��3��6G   $��Fecha fin Tratamiento��6T   �� 59   
   -   5         ��Name$ especialidad��59   <     CuadroTextoAlta#���  6   $��1��6R   $��1��6b   $��7,7,7,7q�6o   �� 5:   
   -   5        $ principioActivo��5:   <   #���  6   $��2��6U   $��1��6e   $��7,7,7,7q�6r   �� 5;   
   -   5        $ dosis��5;   <   #���  6   $��3��6K   $��1��6[   $��7,7,7,7q�6h   �� 5<   
   -   5         ��Name$	 via��5<   <     ComboBoxStyle1#���  6   $��1��6I   $��4��6Y    �� SelectedIndex$ 0��6f   $��7,7,7,7q�6x   �� 5=   
   -   5        $ 
frecuencia��5=   <   #���  6   $��2��6P   $��4��6`   $��7,7,7,7q�6m   �� 5>   
   -   5        $ fechaFinTrat��5>   <   #���  6   $��3��6R   $��4��6b   $��7,7,7,7q�6o   �� 5@   
   -   5         ��Name$ aceptar��5@   7     BotonSimple#���  6   $��Aceptar��6H   $��3��6Z   $��5��6j   $��Right=�6w   6�   5A      5B      