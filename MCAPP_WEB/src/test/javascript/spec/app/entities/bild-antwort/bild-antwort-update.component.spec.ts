/* tslint:disable max-line-length */
import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { McappWebTestModule } from '../../../test.module';
import { BildAntwortUpdateComponent } from 'app/entities/bild-antwort/bild-antwort-update.component';
import { BildAntwortService } from 'app/entities/bild-antwort/bild-antwort.service';
import { BildAntwort } from 'app/shared/model/bild-antwort.model';

describe('Component Tests', () => {
    describe('BildAntwort Management Update Component', () => {
        let comp: BildAntwortUpdateComponent;
        let fixture: ComponentFixture<BildAntwortUpdateComponent>;
        let service: BildAntwortService;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [BildAntwortUpdateComponent]
            })
                .overrideTemplate(BildAntwortUpdateComponent, '')
                .compileComponents();

            fixture = TestBed.createComponent(BildAntwortUpdateComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(BildAntwortService);
        });

        describe('save', () => {
            it(
                'Should call update service on save for existing entity',
                fakeAsync(() => {
                    // GIVEN
                    const entity = new BildAntwort(123);
                    spyOn(service, 'update').and.returnValue(of(new HttpResponse({ body: entity })));
                    comp.bildAntwort = entity;
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
                    const entity = new BildAntwort();
                    spyOn(service, 'create').and.returnValue(of(new HttpResponse({ body: entity })));
                    comp.bildAntwort = entity;
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
