import { CommonModule, NgFor, NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-sprint',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule, CommonModule],
  templateUrl: './sprint.component.html',
  styleUrl: './sprint.component.css'
})
export class SprintComponent {

  sprints: any[] = [];
  editingSprintId: number | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getSprints();
  }

  getSprints() {
    this.http.get<any[]>('https://localhost:5001/api/Sprint')
      .subscribe(
        response => {
          this.sprints = response;
        },
        error => {
          console.error('Error fetching sprints', error);
        }
      );
  }

  createSprint() {
    const newSprint = {
      sprintName: 'New Sprint',
      sprintDetails: 'Details for the new sprint',
      sprintDocumentName: 'Sprint Document Name',
      allocatedDays: 10,
      expectDate: new Date(),
      startDate: new Date(),
      endDate: new Date(),
      approvedBy: 1,
      allocatedTo: 2,
      testedBy: 3,
      isCompleted: false,
      complains: 'No complains'
    };

    this.http.post('https://localhost:5001/api/Sprint', newSprint)
      .subscribe(
        response => {
          console.log('Sprint created', response);
          this.getSprints();
        },
        error => {
          console.error('Error creating sprint', error);
        }
      );
  }

  editSprint(id: number) {
    this.editingSprintId = id;
  }

  cancelEdit() {
    this.editingSprintId = null;
    this.getSprints();
  }

  saveSprint(sprint: any) {
    this.http.put(`https://localhost:5001/api/Sprint/${sprint.id}`, sprint)
      .subscribe(
        response => {
          console.log('Sprint updated', response);
          this.editingSprintId = null;
          this.getSprints();
        },
        error => {
          console.error('Error updating sprint', error);
        }
      );
  }

  deleteSprint(id: number) {
    this.http.delete(`https://localhost:5001/api/Sprint/${id}`)
      .subscribe(
        response => {
          console.log('Sprint deleted', response);
          this.getSprints();
        },
        error => {
          console.error('Error deleting sprint', error);
        }
      );
  }

}
