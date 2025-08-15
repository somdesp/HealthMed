import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { MedicoService } from '../../../core/services/medico.service';
import { ConsultasMarcadasMedico } from '../../../core/model/medico';
import { MatDialog } from '@angular/material/dialog';
import { AgendaComponent } from '../agenda/agenda.component';

@Component({
  selector: 'app-medico',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule], // Importando o CommonModule para usar o pipe 'date'
  templateUrl: './medico.component.html',
  styleUrl: './medico.component.scss'
})
export class MedicoComponent implements OnInit {

  ngOnInit(): void { this.buscaAgendamentos() }
  constructor(private _medicoService: MedicoService, private router: Router, private dialog: MatDialog) { }

  consultas: ConsultasMarcadasMedico[] = [];

  visualizarLaudo(consulta: any) {
    alert(`Visualizando laudo da consulta com ${consulta.paciente}`);
  }

  buscaAgendamentos() {
    this._medicoService.MeusAgendamentos().subscribe({
      next: (response: any) => {
        this.consultas = response;
      }
    });
  }

  buscaAgendas() {

  }

  abrirAgendas() {
    const dialogRef = this.dialog.open(AgendaComponent, {
      width: '600px',
      data: { /* dados que você quiser passar */ }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // lógica após fechamento do modal
        console.log('Horário salvo:', result);
      }
    });
  }
}
