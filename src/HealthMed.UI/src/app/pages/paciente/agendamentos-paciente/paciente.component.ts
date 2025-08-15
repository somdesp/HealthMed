import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { PacienteService } from '../../../core/services/paciente.service';
import { ConsultasMarcadasPaciente, Medico } from '../../../core/model/medico';
import { MatDialog } from '@angular/material/dialog';
import { AgendaMedicoComponent } from '../agenda-medico/agenda-medico.component';

@Component({
  selector: 'app-paciente',
  standalone: true,
  providers: [],
  imports: [CommonModule, RouterModule, FormsModule], // Importando o CommonModule para usar o pipe 'date'
  templateUrl: './paciente.component.html',
  styleUrl: './paciente.component.scss'
})
export class PacienteComponent implements OnInit {
  constructor(private _pacienteService: PacienteService, private router: Router, private dialog: MatDialog) { }
  ngOnInit(): void {
    this.BuscaConsultasMarcadas();
  }

  medicos: Medico[] = [];
  filtro: string = '';
  consultas: ConsultasMarcadasPaciente[] = [];

  visualizar(consulta: any) {
    alert(`Visualizando consulta com ${consulta.medico}`);
  }

  BuscaMedico() {
    this._pacienteService.BuscaMedicos(this.filtro.toLowerCase()).subscribe({
      next: (response: any) => {
        this.medicos = response;
      }
    });
  }

  BuscaConsultasMarcadas() {
    this._pacienteService.BuscaConsultasMarcadas().subscribe({
      next: (response: any) => {
        this.consultas = response;
      }
    });
  }

  abrirAgendas(medicoId: number) {
    const dialogRef = this.dialog.open(AgendaMedicoComponent, {
      width: '600px',
      data: { medicoId: medicoId } // Passando o ID do médico como exemplo
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // lógica após fechamento do modal
        console.log('Horário salvo:', result);
      }
    });
  }
}
