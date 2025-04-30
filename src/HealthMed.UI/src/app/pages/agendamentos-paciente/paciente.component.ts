import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-paciente',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule], // Importando o CommonModule para usar o pipe 'date'
  templateUrl: './paciente.component.html',
  styleUrl: './paciente.component.scss'
})
export class PacienteComponent {
  filtro: string = '';
  consultas = [
    {
      medico: 'Dra. Ana Clara',
      especialidade: 'Cardiologia',
      data: new Date(),
      status: 'pendente',
      laudo: null
    },
    {
      medico: 'Dr. Lucas Silva',
      especialidade: 'Dermatologia',
      data: new Date('2024-12-02'),
      status: 'concluida',
      laudo: 'Laudo aprovado'
    }
  ];

  consultasFiltradas() {
    return this.consultas.filter(
      consulta =>
        consulta.medico.toLowerCase().includes(this.filtro.toLowerCase()) ||
        consulta.especialidade.toLowerCase().includes(this.filtro.toLowerCase())
    );
  }

  visualizar(consulta: any) {
    alert(`Visualizando consulta com ${consulta.medico}`);
  }
}
