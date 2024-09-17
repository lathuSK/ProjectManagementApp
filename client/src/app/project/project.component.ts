import { NgFor, NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-project',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule],
  templateUrl: './project.component.html',
  styleUrl: './project.component.css'
})
export class ProjectComponent {
  projects: any[] = [];
  editingProjectId: number | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getProjects();
  }

  // Fetch projects from the API
  getProjects() {
    this.http.get<any[]>('https://localhost:5001/api/Project')
      .subscribe(
        response => {
          this.projects = response;
        },
        error => {
          console.error('Error fetching projects', error);
        }
      );
  }

  createProject() {
    const newProject = {
      name: 'New Project',
      detail: 'Details for the new project',
      webHostedDetails: 'http://example.com',
      databaseDetails: 'Database info',
      mainDeveloper: 'Main Dev',
      supportDevelopers: 'Support Devs',
      azureDevopsDetails: 'Azure DevOps Details',
      codeLocationInVM: 'Location in VM',
      linkedProjects: 'Linked projects here'
    };

    this.http.post('https://localhost:5001/api/Project', newProject)
      .subscribe(
        response => {
          console.log('Project created', response);
          this.getProjects();
        },
        error => {
          console.error('Error creating project', error);
        }
      );
  }

  editProject(id: number) {
    this.editingProjectId = id;
  }

  cancelEdit() {
    this.editingProjectId = null;
    this.getProjects();
  }

  saveProject(project: any) {
    this.http.put(`https://localhost:5001/api/Project/${project.id}`, project)
      .subscribe(
        response => {
          console.log('Project updated', response);
          this.editingProjectId = null;
          this.getProjects();
        },
        error => {
          console.error('Error updating project', error);
        }
      );
  }

  deleteProject(id: number) {
    this.http.delete(`https://localhost:5001/api/Project/${id}`)
      .subscribe(
        response => {
          console.log('Project deleted', response);
          this.getProjects();
        },
        error => {
          console.error('Error deleting project', error);
        }
      );
  }
}
