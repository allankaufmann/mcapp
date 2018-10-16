/* tslint:disable max-line-length */
import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { McappWebTestModule } from '../../../test.module';
import { TextAntwortUpdateComponent } from 'app/entities/text-antwort/text-antwort-update.component';
import { TextAntwortService } from 'app/entities/text-antwort/text-antwort.service';
import { TextAntwort } from 'app/shared/model/text-antwort.model';

describe('Component Tests', () => {
    describe('TextAntwort Management Update Component', () => {
        let comp: TextAntwortUpdateComponent;
        let fixture: ComponentFixture<TextAntwortUpdateComponent>;
        let service: TextAntwortService;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [TextAntwortUpdateComponent]
            })
                .overrideTemplate(TextAntwortUpdateComponent, '')
                .compileComponents();

            fixture = TestBed.createComponent(TextAntwortUpdateComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(TextAntwortService);
        });

        describe('save', () => {
            it(
                'Should call update service on save for existing entity',
                fakeAsync(() => {
                    // GIVEN
                    const entity = new TextAntwort(123);
                    spyOn(service, 'update').and.returnValue(of(new HttpResponse({ body: entity })));
                    comp.textAntwort = entity;
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
                    const entity = new TextAntwort();
                    spyOn(service, 'create').and.returnValue(of(new HttpResponse({ body: entity })));
                    comp.textAntwort = entity;
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
