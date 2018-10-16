/* tslint:disable max-line-length */
import { ComponentFixture, TestBed, inject, fakeAsync, tick } from '@angular/core/testing';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import { JhiEventManager } from 'ng-jhipster';

import { McappWebTestModule } from '../../../test.module';
import { TextAntwortDeleteDialogComponent } from 'app/entities/text-antwort/text-antwort-delete-dialog.component';
import { TextAntwortService } from 'app/entities/text-antwort/text-antwort.service';

describe('Component Tests', () => {
    describe('TextAntwort Management Delete Component', () => {
        let comp: TextAntwortDeleteDialogComponent;
        let fixture: ComponentFixture<TextAntwortDeleteDialogComponent>;
        let service: TextAntwortService;
        let mockEventManager: any;
        let mockActiveModal: any;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [TextAntwortDeleteDialogComponent]
            })
                .overrideTemplate(TextAntwortDeleteDialogComponent, '')
                .compileComponents();
            fixture = TestBed.createComponent(TextAntwortDeleteDialogComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(TextAntwortService);
            mockEventManager = fixture.debugElement.injector.get(JhiEventManager);
            mockActiveModal = fixture.debugElement.injector.get(NgbActiveModal);
        });

        describe('confirmDelete', () => {
            it(
                'Should call delete service on confirmDelete',
                inject(
                    [],
                    fakeAsync(() => {
                        // GIVEN
                        spyOn(service, 'delete').and.returnValue(of({}));

                        // WHEN
                        comp.confirmDelete(123);
                        tick();

                        // THEN
                        expect(service.delete).toHaveBeenCalledWith(123);
                        expect(mockActiveModal.dismissSpy).toHaveBeenCalled();
                        expect(mockEventManager.broadcastSpy).toHaveBeenCalled();
                    })
                )
            );
        });
    });
});
