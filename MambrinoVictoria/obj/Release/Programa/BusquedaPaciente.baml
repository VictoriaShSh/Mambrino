   M S B A M L   `   `   `  ���� K  GMambrinoVictoria, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:MambrinoVictoria.ProgramaMambrinoVictoria.Programa  0    *MambrinoVictoria.Programa.BusquedaPaciente   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:MambrinoVictoria.Programa     $	��300��$
��1025��  � 	Resources   �� Source�� $ /HojaEstilos/Estilos.xaml?�%    .���  � RowDefinitions �� $��1* ��� $	��60* ��� $	��30* ��� $	��40* ��� $	��30* ��� $	��40* ��� $	��60* � � ColumnDefinitions �� $��5 ��� $	��250 ��� $	��250 ��� $	��250 ��� $	��250 ��� $��5 �.+��� $��1��$��1�����	#FF0E5C05$��4���� $��2��$��1�����	#FF68B6E9$��4���� $��4��$��1�����	#FF68B6E9$��4���� $��3��$��1�����	#FFCADBF3$��4���� $��5��$��1�����	#FFCADBF3$��4���� 
�������$��2q�$��1��$��1��$��4���� 
�������$��2q�$��1��$��2���� 
�������$��2q�$��1��$��3���� 
�������$��2q�$��1��$��4���� 
�������$��2q�$��1��$��5���� 
�������$��2q�$��2��$��2���� 
�������$��2q�$��2��$��3���� 
�������$��2q�$��2��$��4���� 
�������$��2q�$��2��$��5���� 
�������$��2q�$��3��$��2���� 
�������$��2q�$��3��$��3���� 
�������$��2q�$��3��$��4���� 
�������$��2q�$��3��$��5���� 
�������$��2q�$��4��$��2���� 
�������$��2q�$��4��$��3���� 
�������$��2q�$��4��$��4���� 
�������$��2q�$��4��$��5���� -    ��Name$	 nif��   CuadroTextoAlta#���   $��2��$��3���� -   $	 nhc��#���   $��4��$��3���� -    ��Name$ 
zonaBasica��  ComboBoxStyle1#���  $��4��$��5�� �� SelectedIndex$ 0���� -   $ 	areaSalud��#���  $��2��$��5��$ 0����   	TextoAzul#���  $	��NIF��$��3��$��1���� #���  $	��NHC��$��3��$��3���� #���  $��Área de Salud��$��5��$��1���� #���  $��Zona Basica de Salud��$��5��$��3���� $��Búsqueda de pacientes��$��36(�
�������$��1��$��1��$��2��$
��Bold"�$��0,0,0,3q��� $��/Resources/flecha2.png��$
��Left=�$��Center=�$��16��$��22��$��5,0,0,0q�$��2��$��1���� $��/Resources/flecha2.png��$
��Left=�$��Center=�$��16��$��22��$��5,0,0,0q�$��4��$��1���� $��Historia��$��12(�
�������$��2��$��1��$
��Bold"�$��30,0,0,0q��� $��Proceso��$��12(�
�������$��4��$��1��$
��Bold"�$��30,0,0,0q��� -    ��Name$ buscar��  BotonSimple#���  $��4��$��6��$��Buscar��$��30��$��93��$��	125,0,0,0q�