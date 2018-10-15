/* tslint:disable max-line-length */
import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { McappWebTestModule } from '../../../test.module';
import { FrageUpdateComponent } from 'app/entities/frage/frage-update.component';
import { FrageService } from 'app/entities/frage/frage.service';
import { Frage } from 'app/shared/model/frage.model';

describe('Component Tests', () => {
    describe('Frage Management Update Component', () => {
        let comp: FrageUpdateComponent;
        let fixture: ComponentFixture<FrageUpdateComponent>;
        let service: FrageService;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [FrageUpdateComponent]
            })
                .overrideTemplate(FrageUpdateComponent, '')
                .compileComponents();

            fixture = TestBed.createComponent(FrageUpdateComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(FrageService);
        });

        describe('save', () => {
            it(
                'Should call update service on save for existing entity',
                fakeAsync(() => {
                    // GIVEN
                    const entity = new Frage(123);
                    spyOn(service, 'update').and.returnValue(of(new HttpResponse({ body: entity })));
                    comp.frage = entity;
                    // WHEN
                    comp.save();
                    tick(); // simulate async

                    // THEN
                    expect(service.update).toHaveBeenCalledWith(entity);
                    expect(comp.isSaving).toEqual(false);
                })
            );

            it(
                'Should call create service on save for new entity',
                fakeAsync(() => {
                    // GIVEN
                    const entity = new Frage();
                    spyOn(service, 'create').and.returnValue(of(new HttpResponse({ body: entity })));
                    comp.frage = entity;
                    // WHEN
                    comp.save();
                    tick(); // simulate async

                    // THEN
                    expect(service.create).toHaveBeenCalledWith(entity);
                    expect(comp.isSaving).toEqual(false);
                })
            );
        });
    });
});
