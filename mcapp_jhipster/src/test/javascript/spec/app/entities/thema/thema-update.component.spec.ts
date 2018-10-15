/* tslint:disable max-line-length */
import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { McappWebTestModule } from '../../../test.module';
import { ThemaUpdateComponent } from 'app/entities/thema/thema-update.component';
import { ThemaService } from 'app/entities/thema/thema.service';
import { Thema } from 'app/shared/model/thema.model';

describe('Component Tests', () => {
    describe('Thema Management Update Component', () => {
        let comp: ThemaUpdateComponent;
        let fixture: ComponentFixture<ThemaUpdateComponent>;
        let service: ThemaService;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [ThemaUpdateComponent]
            })
                .overrideTemplate(ThemaUpdateComponent, '')
                .compileComponents();

            fixture = TestBed.createComponent(ThemaUpdateComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(ThemaService);
        });

        describe('save', () => {
            it(
                'Should call update service on save for existing entity',
                fakeAsync(() => {
                    // GIVEN
                    const entity = new Thema(123);
                    spyOn(service, 'update').and.returnValue(of(new HttpResponse({ body: entity })));
                    comp.thema = entity;
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
                    const entity = new Thema();
                    spyOn(service, 'create').and.returnValue(of(new HttpResponse({ body: entity })));
                    comp.thema = entity;
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
