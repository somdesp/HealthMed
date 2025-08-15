import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MedicoService } from '../../../core/services/medico.service';
import { CommonModule } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-agenda',
  standalone: true,
  imports: [FormsModule, CommonModule,
    ReactiveFormsModule],
  templateUrl: './agenda.component.html',
  styleUrl: './agenda.component.scss'
})
export class AgendaComponent {


  Agendas: Agenda[] = [];
  NovaAgenda: any;

  constructor(private medicoService: MedicoService, private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<AgendaComponent>
  ) { }

  ngOnInit(): void {
    this.loadAgendas();
  }

  loadAgendas() {
    this.medicoService.getAgendas().subscribe({
      next: (response: any) => {
        this.Agendas = response.agendas;
      }
    });
  }

  deletaAgenda(id: number) {

    this.medicoService.deletaAgenda(id).subscribe(() => {
      this.loadAgendas();
    });
  }

  adicionarAgenda() {
    this.medicoService.addAgendas(this.NovaAgenda).subscribe({
      next: (response: any) => {
        this.loadAgendas();

      }
    });
  }

  fechar() {
    this.dialogRef.close();
  }

}


export interface Agenda {
  id: number
  dataHora: string
}

export interface HorariosDisponiveis {
  id: number
  medicoId: number
  dataHora: string
  medico: string
  valorConsulta: number
}
