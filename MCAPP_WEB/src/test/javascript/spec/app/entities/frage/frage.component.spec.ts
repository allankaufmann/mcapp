/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { HttpHeaders, HttpResponse } from '@angular/common/http';

import { McappWebTestModule } from '../../../test.module';
import { FrageComponent } from 'app/entities/frage/frage.component';
import { FrageService } from 'app/entities/frage/frage.service';
import { Frage } from 'app/shared/model/frage.model';

describe('Component Tests', () => {
    describe('Frage Management Component', () => {
        let comp: FrageComponent;
        let fixture: ComponentFixture<FrageComponent>;
        let service: FrageService;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [FrageComponent],
                providers: []
            })
                .overrideTemplate(FrageComponent, '')
                .compileComponents();

            fixture = TestBed.createComponent(FrageComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(FrageService);
        });

        it('Should call load all on init', () => {
            // GIVEN
            const headers = new HttpHeaders().append('link', 'link;link');
            spyOn(service, 'query').and.returnValue(
                of(
                    new HttpResponse({
                        body: [new Frage(123)],
                        headers
                    })
                )
            );

            // WHEN
            comp.ngOnInit();

            // THEN
            expect(service.query).toHaveBeenCalled();
            expect(comp.frages[0]).toEqual(jasmine.objectContaining({ id: 123 }));
        });
    });
});
