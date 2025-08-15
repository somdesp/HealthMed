import { Component, Inject } from '@angular/core';
import { PacienteService } from '../../../core/services/paciente.service';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Agenda, AgendaComponent, HorariosDisponiveis } from '../../medico/agenda/agenda.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-agenda-medico',
  standalone: true,
  imports: [FormsModule, CommonModule,
    ReactiveFormsModule],
  templateUrl: './agenda-medico.component.html',
  styleUrl: './agenda-medico.component.scss'
})
export class AgendaMedicoComponent {

  Agendas: Agenda[] = [];
  NovaAgenda: any;
  HorariosDisponiveis: HorariosDisponiveis[] = [];

  constructor(private _pacienteService: PacienteService, private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<AgendaComponent>
  ) { }

  ngOnInit(): void {
    this.loadAgendas();
  }

  loadAgendas() {
    this._pacienteService.AgendasMedico(this.data.medicoId).subscribe({
      next: (response: any) => {
        this.HorariosDisponiveis = response.agendas;
      }
    });
  }

  // deletaAgenda(id: number) {

  //   this.medicoService.deletaAgenda(id).subscribe(() => {
  //     this.loadAgendas();
  //   });
  // }

  // adicionarAgenda() {
  //   this.medicoService.addAgendas(this.NovaAgenda).subscribe({
  //     next: (response: any) => {
  //       this.loadAgendas();

  //     }
  //   });
  // }

  fechar() {
    this.dialogRef.close();
  }
}
