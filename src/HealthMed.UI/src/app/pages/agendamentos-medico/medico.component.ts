import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-medico',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule], // Importando o CommonModule para usar o pipe 'date'
  templateUrl: './medico.component.html',
  styleUrl: './medico.component.scss'
})
export class MedicoComponent {

  consultas = [
    {
      paciente: 'João Silva',
      especialidade: 'Cardiologia',
      data: new Date(),
      status: 'pendente'
    },
    {
      paciente: 'Maria Oliveira',
      especialidade: 'Dermatologia',
      data: new Date('2024-12-05'),
      status: 'confirmada'
    }
  ];

  visualizarLaudo(consulta: any) {
    alert(`Visualizando laudo da consulta com ${consulta.paciente}`);
  }
}
